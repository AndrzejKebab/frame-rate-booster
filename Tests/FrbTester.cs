using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

/// <summary>
///     Runs a set of operations a specific number of times and records their results and the time it took to run them.
///     That data is written as two txt files in the project's root.
/// </summary>
public class FrbTester : MonoBehaviour
{
	[FormerlySerializedAs("operationsNumber")] public int      OperationsNumber = 1000000;
	public  Dropdown Dropdown;

	private string stats   = "";
	private string results = "";

	private void Start()
	{
		//Set all the Methods in the DropDown
		Dropdown.options.Clear();
		foreach (var methodName in Enum.GetNames(typeof(Methods)))
			Dropdown.options.Add(new Dropdown.OptionData(methodName));

		StartCoroutine(RunTestsOnMethod(0));
	}

	private IEnumerator RunTestsOnMethod(Methods method)
	{
		//Update display
		Dropdown.value = (int)method;

		float lastUpdateDuration = 0;

		var vec4Result    = default(Vector4);
		var vec3Result    = default(Vector3);
		var vec2Result    = default(Vector2);
		var vec3IntResult = default(Vector3Int);
		var vec2IntResult = default(Vector2Int);
		var quatResult    = default(Quaternion);
		var color32Result = default(Color32);
		var colorResult   = default(Color);
		var fResult       = 0.0f;
		var boundsResult  = default(Bounds);

		for (var k = 0; k < 5; k++) //Multiple iterations to allow warmup
		{
			//Data used in tests
			var         vec4    = new Vector4(5, 20, 3, 17);
			var         vec3    = new Vector3(5, 20, 3);
			var         vec2    = new Vector2(5, 20);
			var         vec3Int = new Vector3Int(5, 20, 3);
			var         vec2Int = new Vector2Int(5, 20);
			var         color32 = new Color32(5, 20, 3, 17);
			Quaternion  quat    = Quaternion.identity;
			const float f       = 0.3f;
			const int   j       = 3;
			var         bounds  = new Bounds(Vector3.one, Vector3.one);


			var stopWatch = new Stopwatch();
			stopWatch.Start();

			//the actual test
			switch (method)
			{
				case Methods.Vec4Plus:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = vec4 + vec4;
					break;
				case Methods.Vec4Minus:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = vec4 - vec4;
					break;
				case Methods.Vec4Multiply1:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = vec4 * f;
					break;
				case Methods.Vec4Multiply2:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = f * vec4;
					break;
				case Methods.Vec4UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = -vec4;
					break;
				case Methods.Vec4Division:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = vec4 / f;
					break;
				case Methods.Vec4Magnitude:
					for (var i = 0; i < OperationsNumber; i++) fResult = Vector4.Magnitude(vec4);
					break;
				case Methods.Vec4MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) fResult = vec4.magnitude;
					break;
				case Methods.Vec4Normalize:
					for (var i = 0; i < OperationsNumber; i++) vec4.Normalize();
					break;
				case Methods.Vec4NormalizeStatic:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = Vector4.Normalize(vec4);
					break;
				case Methods.Vec4NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = vec4.normalized;
					break;
				case Methods.Vec4Distance:
					for (var i = 0; i < OperationsNumber; i++) fResult = Vector4.Distance(vec4, vec4);
					break;
				case Methods.Vec4Scale:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = Vector4.Scale(vec4, vec4);
					break;
				case Methods.Vec4LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = Vector4.LerpUnclamped(vec4, vec4, f);
					break;
				case Methods.Vec4Lerp:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = Vector4.Lerp(vec4, vec4, f);
					break;
				case Methods.Vec4Min:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = Vector4.Min(vec4, vec4);
					break;
				case Methods.Vec4Max:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = Vector4.Max(vec4, vec4);
					break;
				case Methods.Vec3Plus:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = vec3 + vec3;
					break;
				case Methods.Vec3Minus:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = vec3 - vec3;
					break;
				case Methods.Vec3Multiply1:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = vec3 * f;
					break;
				case Methods.Vec3Multiply2:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = f * vec3;
					break;
				case Methods.Vec3UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = -vec3;
					break;
				case Methods.Vec3Division:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = vec3 / f;
					break;
				case Methods.Vec3Magnitude:
					for (var i = 0; i < OperationsNumber; i++) fResult = Vector3.Magnitude(vec3);
					break;
				case Methods.Vec3MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) fResult = vec3.magnitude;
					break;
				case Methods.Vec3Normalize:
					for (var i = 0; i < OperationsNumber; i++) vec3.Normalize();
					break;
				case Methods.Vec3NormalizeStatic:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = Vector3.Normalize(vec3);
					break;
				case Methods.Vec3NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = vec3.normalized;
					break;
				case Methods.Vec3Distance:
					for (var i = 0; i < OperationsNumber; i++) fResult = Vector3.Distance(vec3, vec3);
					break;
				case Methods.Vec3LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = Vector3.LerpUnclamped(vec3, vec3, f);
					break;
				case Methods.Vec3Lerp:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = Vector3.Lerp(vec3, vec3, f);
					break;
				case Methods.Vec3Cross:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = Vector3.Cross(vec3, vec3);
					break;
				case Methods.Vec3Scale:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = Vector3.Scale(vec3, vec3);
					break;
				case Methods.Vec3Min:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = Vector3.Min(vec3, vec3);
					break;
				case Methods.Vec3Max:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = Vector3.Max(vec3, vec3);
					break;
				case Methods.Vec2Plus:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec2 + vec2;
					break;
				case Methods.Vec2Minus:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec2 - vec2;
					break;
				case Methods.Vec2Multiply1:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec2 * f;
					break;
				case Methods.Vec2Multiply2:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = f * vec2;
					break;
				case Methods.Vec2Multiply3:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec2 * vec2;
					break;
				case Methods.Vec2UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = -vec2;
					break;
				case Methods.Vec2Division1:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec2 / f;
					break;
				case Methods.Vec2Division2:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec2 / vec2;
					break;
				case Methods.Vec2MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) fResult = vec2.magnitude;
					break;
				case Methods.Vec2Normalize:
					for (var i = 0; i < OperationsNumber; i++) vec2.Normalize();
					break;
				case Methods.Vec2NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec2.normalized;
					break;
				case Methods.Vec2Perpendicular:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = Vector2.Perpendicular(vec2);
					break;
				case Methods.Vec2Distance:
					for (var i = 0; i < OperationsNumber; i++) fResult = Vector2.Distance(vec2, vec2);
					break;
				case Methods.Vec2LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = Vector2.LerpUnclamped(vec2, vec2, f);
					break;
				case Methods.Vec2Lerp:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = Vector2.Lerp(vec2, vec2, f);
					break;
				case Methods.Vec2Scale:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = Vector2.Scale(vec2, vec2);
					break;
				case Methods.Vec2Min:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = Vector2.Min(vec2, vec2);
					break;
				case Methods.Vec2Max:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = Vector2.Max(vec2, vec2);
					break;
				case Methods.Vec3IntPlus:
					for (var i = 0; i < OperationsNumber; i++) vec3IntResult = vec3Int + vec3Int;
					break;
				case Methods.Vec3IntMinus:
					for (var i = 0; i < OperationsNumber; i++) vec3IntResult = vec3Int - vec3Int;
					break;
				case Methods.Vec3IntMultiply1:
					for (var i = 0; i < OperationsNumber; i++) vec3IntResult = vec3Int * vec3Int;
					break;
				case Methods.Vec3IntMultiply2:
					for (var i = 0; i < OperationsNumber; i++) vec3IntResult = vec3Int * j;
					break;
				case Methods.Vec3IntMagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) fResult = vec3Int.magnitude;
					break;
				case Methods.Vec3IntDistance:
					for (var i = 0; i < OperationsNumber; i++) fResult = Vector3Int.Distance(vec3Int, vec3Int);
					break;
				case Methods.Vec3IntScale:
					for (var i = 0; i < OperationsNumber; i++) vec3IntResult = Vector3Int.Scale(vec3Int, vec3Int);
					break;
				case Methods.Vec3IntMin:
					for (var i = 0; i < OperationsNumber; i++) vec3IntResult = Vector3Int.Min(vec3Int, vec3Int);
					break;
				case Methods.Vec3IntMax:
					for (var i = 0; i < OperationsNumber; i++) vec3IntResult = Vector3Int.Max(vec3Int, vec3Int);
					break;
				case Methods.Vec2IntPlus:
					for (var i = 0; i < OperationsNumber; i++) vec2IntResult = vec2Int + vec2Int;
					break;
				case Methods.Vec2IntMinus:
					for (var i = 0; i < OperationsNumber; i++) vec2IntResult = vec2Int - vec2Int;
					break;
				case Methods.Vec2IntMultiply1:
					for (var i = 0; i < OperationsNumber; i++) vec2IntResult = vec2Int * vec2Int;
					break;
				case Methods.Vec2IntMultiply2:
					for (var i = 0; i < OperationsNumber; i++) vec2IntResult = vec2Int * j;
					break;
				case Methods.Vec2IntMagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) fResult = vec2Int.magnitude;
					break;
				case Methods.Vec2IntDistance:
					for (var i = 0; i < OperationsNumber; i++) fResult = Vector2Int.Distance(vec2Int, vec2Int);
					break;
				case Methods.Vec2IntScale:
					for (var i = 0; i < OperationsNumber; i++) vec2IntResult = Vector2Int.Scale(vec2Int, vec2Int);
					break;
				case Methods.Vec2IntMin:
					for (var i = 0; i < OperationsNumber; i++) vec2IntResult = Vector2Int.Min(vec2Int, vec2Int);
					break;
				case Methods.Vec2IntMax:
					for (var i = 0; i < OperationsNumber; i++) vec2IntResult = Vector2Int.Max(vec2Int, vec2Int);
					break;
				case Methods.QuaternionMultiply:
					for (var i = 0; i < OperationsNumber; i++) quatResult = quat * quat;
					break;
				case Methods.Color32LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++)
						color32Result = Color32.LerpUnclamped(color32, color32, f);
					break;
				case Methods.Color32Lerp:
					for (var i = 0; i < OperationsNumber; i++) color32Result = Color32.Lerp(color32, color32, f);
					break;
				case Methods.ImplicitV2V3:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec3;
					break;
				case Methods.ImplicitV2V4:
					for (var i = 0; i < OperationsNumber; i++) vec2Result = vec4;
					break;
				case Methods.ImplicitV3V2:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = vec2;
					break;
				case Methods.ImplicitV3V4:
					for (var i = 0; i < OperationsNumber; i++) vec3Result = vec4;
					break;
				case Methods.ImplicitV4V2:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = vec2;
					break;
				case Methods.ImplicitV4V3:
					for (var i = 0; i < OperationsNumber; i++) vec4Result = vec3;
					break;
				case Methods.ImplicitC32C:
					for (var i = 0; i < OperationsNumber; i++) color32Result = Color.magenta;
					break;
				case Methods.ImplicitCC32:
					for (var i = 0; i < OperationsNumber; i++) colorResult = color32;
					break;
				case Methods.BoundsExpandV:
					for (var i = 0; i < OperationsNumber; i++) bounds.Expand(Vector3.one);
					break;
				case Methods.BoundsExpandF:
					for (var i = 0; i < OperationsNumber; i++) bounds.Expand(1f);
					break;
				default:
					throw new ArgumentOutOfRangeException(method.ToString());
			}

			stopWatch.Stop();

			boundsResult = bounds;

			lastUpdateDuration = (float)stopWatch.ElapsedTicks / TimeSpan.TicksPerMillisecond;
		}

		stats += $"{method}\n{lastUpdateDuration:F1} ms\n";

		var result = "";
		result += vec4Result;
		result += "\n";
		result += vec3Result;
		result += "\n";
		result += vec2Result;
		result += "\n";
		result += vec3IntResult;
		result += "\n";
		result += vec2IntResult;
		result += "\n";
		result += quatResult;
		result += "\n";
		result += color32Result;
		result += "\n";
		result += colorResult;
		result += "\n";
		result += boundsResult;
		result += "\n";
		result += fResult;
		result += "\n";
		result += "******************\n";

		results += $"{method}\n{result}\n";

		//Run next method test or if finished save results and exit
		Methods maxEnumValue = new List<Methods>((Methods[])Enum.GetValues(typeof(Methods))).Max();
		if (method < maxEnumValue)
		{
			method = method + 1;
			yield return new WaitForEndOfFrame();
			StartCoroutine(RunTestsOnMethod(method));
		}
		else
		{
			const string statsFilePath = "./Assets/stats.txt";
			File.WriteAllText(statsFilePath, stats);
			Debug.Log("stats saved as " + statsFilePath);

			const string resultsFilePath = "./Assets/results.txt";
			File.WriteAllText(resultsFilePath, results);
			Debug.Log("results saved as " + resultsFilePath);

			Application.Quit();
		}
	}
}

/// <summary>
///     A list of methods to test in the script bellow
/// </summary>
public enum Methods
{
	//Vector4
	Vec4Plus,
	Vec4Minus,
	Vec4Multiply1,
	Vec4Multiply2,
	Vec4UnaryNegation,
	Vec4Division,
	Vec4Magnitude,
	Vec4MagnitudeProp,
	Vec4Normalize,
	Vec4NormalizeStatic,
	Vec4NormalizedProp,
	Vec4Distance,
	Vec4LerpUnclamped,
	Vec4Lerp,
	Vec4Scale,
	Vec4Min,
	Vec4Max,

	//Vector3
	Vec3Plus,
	Vec3Minus,
	Vec3Multiply1,
	Vec3Multiply2,
	Vec3UnaryNegation,
	Vec3Division,
	Vec3Magnitude,
	Vec3MagnitudeProp,
	Vec3Normalize,
	Vec3NormalizeStatic,
	Vec3NormalizedProp,
	Vec3Distance,
	Vec3LerpUnclamped,
	Vec3Lerp,
	Vec3Cross,
	Vec3Scale,
	Vec3Min,
	Vec3Max,

	//Vector2
	Vec2Plus,
	Vec2Minus,
	Vec2Multiply1,
	Vec2Multiply2,
	Vec2Multiply3,
	Vec2UnaryNegation,
	Vec2Division1,
	Vec2Division2,
	Vec2MagnitudeProp,
	Vec2Normalize,
	Vec2NormalizedProp,
	Vec2Perpendicular,
	Vec2Distance,
	Vec2LerpUnclamped,
	Vec2Lerp,
	Vec2Scale,
	Vec2Min,
	Vec2Max,

	//Vector3Int
	Vec3IntPlus,
	Vec3IntMinus,
	Vec3IntMultiply1,
	Vec3IntMultiply2,
	Vec3IntMagnitudeProp,
	Vec3IntDistance,
	Vec3IntScale,
	Vec3IntMin,
	Vec3IntMax,

	//Vector2Int
	Vec2IntPlus,
	Vec2IntMinus,
	Vec2IntMultiply1,
	Vec2IntMultiply2,
	Vec2IntMagnitudeProp,
	Vec2IntDistance,
	Vec2IntScale,
	Vec2IntMin,
	Vec2IntMax,

	//Quaternion
	QuaternionMultiply,

	//Color32
	Color32LerpUnclamped,
	Color32Lerp,

	//Vector implicit operators
	ImplicitV2V3,
	ImplicitV2V4,
	ImplicitV3V2,
	ImplicitV3V4,
	ImplicitV4V2,
	ImplicitV4V3,
	ImplicitC32C,
	ImplicitCC32,

	//Bounds
	BoundsExpandV,
	BoundsExpandF
}