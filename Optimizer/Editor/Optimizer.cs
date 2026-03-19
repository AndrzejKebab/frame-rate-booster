using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Debug = UnityEngine.Debug;

namespace ToolBuddy.FrameRateBooster.Optimizer
{
	public class Optimizer : IPostBuildPlayerScriptDLLs
	{
		#region Unity callbacks

		public int callbackOrder { get; }

		public void OnPostBuildPlayerScriptDLLs(BuildReport report)
		{
			if (PlayerSettings.GetScriptingBackend(NamedBuildTarget
				                                       .FromBuildTargetGroup(report.summary.platformGroup)) !=
			    ScriptingImplementation.IL2CPP) return;
			var pathToBuiltProject = report.GetFiles().FirstOrDefault(r => r.path.Contains("StagingArea")).path;

			//Enabling the code line bellow will run the optimization in this case, but builds fail at linking step, and I don't know why, so I am ignoring the optimization.
			//OptimizeBuild(report.summary.platform, Path.GetDirectoryName(Path.GetFullPath(pathToBuiltProject)));
			Debug.LogWarning(!string.IsNullOrEmpty(pathToBuiltProject)
				                 ? "[Frame Rate Booster] FRB can modify IL2CPP builds only on Unity versions between 2019.3 and 2020.3 inclusive."
				                 : "[Frame Rate Booster] Could not find path for StagingArea");

			Debug.LogWarning("[Frame Rate Booster] Using FRB on IL2CPP can make builds slower. More details at this link: https://forum.curvyeditor.com/thread-861.html");
		}

		[PostProcessBuild(1)]
		public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
		{
			if (PlayerSettings.GetScriptingBackend(NamedBuildTarget.FromBuildTargetGroup(BuildPipeline
				                                       .GetBuildTargetGroup(target))) ==
			    ScriptingImplementation.IL2CPP) return;

			OptimizeBuild(target, Path.GetDirectoryName(Path.GetFullPath(pathToBuiltProject)));
		}

		#endregion

		private static void OptimizeBuild(BuildTarget target, string buildDirectoryPath)
		{
			if (target == BuildTarget.Android)
			{
				Debug.LogWarning("[Frame Rate Booster] Automatic optimization of Android builds is not supported");
				Debug.Log(
				          @"[Frame Rate Booster] You can still optimize it manually, by unpacking the apk file, run the Optimize method on the content of the unpacked apk (specifically the assets\bin\Data folder) and then repack the apk. I personally have 0 experience making android apk files, so I can't help much more with this subject. If you find a way to automate this, please let me know.");
				return;
			}

			const bool optimizationInOwnAssembly = true;

			const string targetAssemblyName = "UnityEngine.CoreModule.dll";

			Debug.Log("[Frame Rate Booster] Started post build optimization");
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var allAssembliesPaths = Directory.GetFiles(buildDirectoryPath, "*.dll", SearchOption.AllDirectories);

			string optimizationsAssemblyPath;
			{
				const string optimizationsAssemblyName = "FrameRateBooster.Optimizations.dll";

				if (!GetUniqueTargetAssembly(optimizationsAssemblyName, allAssembliesPaths, buildDirectoryPath,
				                             "the assembly containing the optimizations",
				                             out optimizationsAssemblyPath))
					return;
			}

			string targetAssemblyPath;
			{
				if (!GetUniqueTargetAssembly(targetAssemblyName, allAssembliesPaths, buildDirectoryPath,
				                             "the assembly to optimize",
				                             out targetAssemblyPath))
					return;
			}

			var optimizedMethodsCount = Optimize(optimizationsAssemblyPath, targetAssemblyPath,
			                                     optimizationInOwnAssembly,
			                                     !optimizationInOwnAssembly);

			stopWatch.Stop();

			Debug.Log("[Frame Rate Booster] Finished post build optimization. Operation took " +
			          stopWatch.ElapsedMilliseconds +
			          " milliseconds and optimized " + optimizedMethodsCount + " methods and properties");
		}

		private static bool GetUniqueTargetAssembly(string targetAssemblyName, IEnumerable<string> allAssembliesPaths,
		                                            string buildDirectory, string assemblyDescription,
		                                            out string targetAssemblyPath)
		{
			List<string> assembliesToOptimize = allAssembliesPaths.Where(s => s.Contains(targetAssemblyName)).ToList();

			switch (assembliesToOptimize.Count())
			{
				case 1:
					targetAssemblyPath = assembliesToOptimize.First();
					break;
				case 0:
					targetAssemblyPath = null;
					Debug.LogError(string.Format("[Frame Rate Booster] Couldn't locate recursively {2} {0} in the build folder {1}",
					                             targetAssemblyName, buildDirectory, assemblyDescription));
					return false;
				default:
					targetAssemblyPath = null;
					Debug.LogError(string.Format("[Frame Rate Booster] Located recursively multiple occurrences of {2} {0} in the build folder {1}",
					                             targetAssemblyName, buildDirectory, assemblyDescription));
					return false;
			}

			return true;
		}

		/// <summary>
		///     Replaces non optimized Unity operators (in target assembly) with optimized ones (from optimizations assembly)
		/// </summary>
		/// <param name="optimizationsAssemblyPath">The path to the assembly containing the optimized version of Unity's opertors</param>
		/// <param name="targetAssemblyPath">The path of the assembly to apply the optimizations on</param>
		/// <param name="deleteOptimizationsAssembly">
		///     Should the optimizations assembly be deleted after the optimization process
		///     is finished?
		/// </param>
		/// <param name="trimOptimizationsAssembly">
		///     Should the optimized operators be removed from the optimizations module after
		///     the  optimization process is finished?
		/// </param>
		/// <returns>The number of optimized methods</returns>
		public static int Optimize(string optimizationsAssemblyPath,   string targetAssemblyPath,
		                           bool   deleteOptimizationsAssembly, bool   trimOptimizationsAssembly)
		{
			const string optimizedNameSpace = "ToolBuddy.FrameRateBooster.Optimizations";
			const string originalNameSpace  = "UnityEngine";

			var optimizedMethodsCount = 0;

			var resolver = new DefaultAssemblyResolver();
			resolver.AddSearchDirectory(Path.GetDirectoryName(targetAssemblyPath));
			var readerParams = new ReaderParameters { ReadWrite = true, AssemblyResolver = resolver };

			using ModuleDefinition optimizedModuleDefinition =
				ModuleDefinition.ReadModule(optimizationsAssemblyPath,
				                            new ReaderParameters
				                            { ReadWrite = trimOptimizationsAssembly, AssemblyResolver = resolver });
			using (ModuleDefinition originalModule =
			       ModuleDefinition.ReadModule(targetAssemblyPath, readerParams))
			{
				foreach (TypeDefinition optimizedType in optimizedModuleDefinition.Types)
					if (optimizedType.Namespace == optimizedNameSpace)
					{
						TypeDefinition originalType =
							GetOriginalTypeIfAny(originalModule, originalNameSpace, optimizedType);

						if (originalType == null) continue;
						foreach (MethodDefinition optimizedMethod in optimizedType.Methods)
						{
							MethodDefinition method =
								originalType.Methods.SingleOrDefault(m => m.Name == optimizedMethod.Name &&
								                                          m.ReturnType.Name ==
								                                          optimizedMethod.ReturnType.Name &&
								                                          m.Parameters.Count ==
								                                          optimizedMethod.Parameters.Count &&
								                                          m.Parameters
								                                           .Select(p => p.ParameterType.Name)
								                                           .SequenceEqual(optimizedMethod.Parameters
									                                           .Select(p => p.ParameterType
										                                           .Name)));


							if (method == null)
							{
								Debug.Log(string
									          .Format("[Frame Rate Booster] Couldn't find in assembly {2} any method to optimize that matches {0}.{1}. This optimization will be skipped.",
									                  optimizedMethod.DeclaringType, optimizedMethod.Name,
									                  originalModule.Name));
								continue;
							}

							method.Body.Variables.Clear();
							foreach (VariableDefinition variable in optimizedMethod.Body.Variables)
							{
								if (variable.VariableType.Namespace == optimizedNameSpace)
									variable.VariableType =
										GetOriginalType(originalModule, originalNameSpace, variable.VariableType);

								method.Body.Variables.Add(variable);
							}

							method.Body.MaxStackSize = optimizedMethod.Body.MaxStackSize;

							method.Body.Instructions.Clear();
							foreach (Instruction instruction in optimizedMethod.Body.Instructions)
							{
								Instruction newInstruction = instruction.Operand switch
								                             {
									                             // 1. Translates variables (e.g. c.r) from ToolBuddy structs to UnityEngine structs
									                             FieldReference fieldReference when
										                             fieldReference.DeclaringType.Namespace ==
										                             optimizedNameSpace =>
										                             Instruction.Create(instruction.OpCode,
										                              new FieldReference(fieldReference.Name,
										                               originalModule
											                               .ImportReference(fieldReference
												                               .FieldType),
										                               GetOriginalType(originalModule,
										                                originalNameSpace,
										                                fieldReference.DeclaringType))),
									                             // 2. Safely imports external fields
									                             FieldReference fieldReference =>
										                             Instruction.Create(instruction.OpCode,
										                              originalModule
											                              .ImportReference(fieldReference)),
									                             // 3. Safely imports external methods (This fixes MathF.Max and MathF.Min crashing!)
									                             MethodReference methodReference =>
										                             Instruction.Create(instruction.OpCode,
										                              originalModule
											                              .ImportReference(methodReference)),
									                             // 4. Translates structs for `result = default;` (This fixes the 'Declared in another module' ArgumentException!)
									                             TypeReference
										                             {
											                             Namespace: optimizedNameSpace
										                             } typeReference =>
										                             Instruction.Create(instruction.OpCode,
										                              GetOriginalType(originalModule,
										                               originalNameSpace, typeReference)),
									                             // 5. Safely imports external types
									                             TypeReference typeReference =>
										                             Instruction.Create(instruction.OpCode,
										                              originalModule
											                              .ImportReference(typeReference)),
									                             _ => instruction
								                             };

								method.Body.GetILProcessor().Append(newInstruction);
							}

							method.ImplAttributes |= MethodImplAttributes.AggressiveInlining;
							method.ImplAttributes &= ~MethodImplAttributes.NoInlining;

							optimizedMethodsCount++;
						}
					}

				if (optimizedMethodsCount == 0)
					Debug.LogError("[Frame Rate Booster] Couldn't find any method to optimize. This is not supposed to happen. Please report that to the asset creator.");

				originalModule.Write();
			}

			if (deleteOptimizationsAssembly)
			{
				optimizedModuleDefinition.Dispose();
				File.Delete(optimizationsAssemblyPath);
			}
			else if (trimOptimizationsAssembly)
			{
				var indicesToRemove = new List<int>();
				for (var i = 0; i < optimizedModuleDefinition.Types.Count; i++)
				{
					TypeDefinition optimizedType = optimizedModuleDefinition.Types[i];
					if (optimizedType.Namespace == optimizedNameSpace)
						indicesToRemove.Add(i);
				}

				indicesToRemove.Sort();
				while (indicesToRemove.Any())
				{
					optimizedModuleDefinition.Types.RemoveAt(indicesToRemove.Last());
					indicesToRemove.Remove(indicesToRemove.Last());
				}

				//TODO assure toi que la taille de assemblyCsharp ne grandit pas
				optimizedModuleDefinition.Write();
			}

			return optimizedMethodsCount;
		}

		private static TypeReference GetOriginalType(ModuleDefinition originalModule, string originalNameSpace,
		                                             TypeReference    optimizedType)
		{
			return originalModule.Types.Single(t => t.Name == optimizedType.Name && t.Namespace == originalNameSpace);
		}

		private static TypeDefinition GetOriginalTypeIfAny(ModuleDefinition originalModule, string originalNameSpace,
		                                                   TypeDefinition   optimizedType)
		{
			return originalModule.Types.SingleOrDefault(t => t.Name == optimizedType.Name &&
			                                                 t.Namespace == originalNameSpace);
		}
	}
}