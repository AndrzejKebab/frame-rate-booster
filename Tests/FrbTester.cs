using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

// Aliases
using UVector2 = UnityEngine.Vector2;
using UVector3 = UnityEngine.Vector3;
using UVector4 = UnityEngine.Vector4;
using UVector2Int = UnityEngine.Vector2Int;
using UVector3Int = UnityEngine.Vector3Int;
using UQuaternion = UnityEngine.Quaternion;
using UColor = UnityEngine.Color;
using UColor32 = UnityEngine.Color32;
using UBounds = UnityEngine.Bounds;
using OVector2 = ToolBuddy.FrameRateBooster.Optimizations.Vector2;
using OVector3 = ToolBuddy.FrameRateBooster.Optimizations.Vector3;
using OVector4 = ToolBuddy.FrameRateBooster.Optimizations.Vector4;
using OVector2Int = ToolBuddy.FrameRateBooster.Optimizations.Vector2Int;
using OVector3Int = ToolBuddy.FrameRateBooster.Optimizations.Vector3Int;
using OQuaternion = ToolBuddy.FrameRateBooster.Optimizations.Quaternion;
using OColor = ToolBuddy.FrameRateBooster.Optimizations.Color;
using OColor32 = ToolBuddy.FrameRateBooster.Optimizations.Color32;
using OBounds = ToolBuddy.FrameRateBooster.Optimizations.Bounds;

public class FrbTester : MonoBehaviour
{
	public int      OperationsNumber = 1000000;
	public Dropdown Dropdown;

	private string csvStats    = "Method,Unity Time (ms),Optimized Time (ms),Performance Gain\n";
	private string textResults = "";

	private void Start()
	{
		Dropdown.options.Clear();
		foreach (var methodName in Enum.GetNames(typeof(Methods)))
			Dropdown.options.Add(new Dropdown.OptionData(methodName));

		StartCoroutine(RunTestsOnMethod(0));
	}

	private IEnumerator RunTestsOnMethod(Methods method)
	{
		Dropdown.value = (int)method;
		IFormatProvider inv = CultureInfo.InvariantCulture;

		float lastUnityTime = 0;
		float lastOptTime   = 0;

		// Result Variables
		UVector4    uVec4Result    = default;
		UVector3    uVec3Result    = default;
		UVector2    uVec2Result    = default;
		UVector3Int uVec3IntResult = default;
		UVector2Int uVec2IntResult = default;
		UQuaternion uQuatResult    = default;
		UColor32    uColor32Result = default;
		UColor      uColorResult   = default;
		UBounds     uBoundsResult  = default;
		var       uFloatResult   = 0.0f;

		OVector4    oVec4Result    = default;
		OVector3    oVec3Result    = default;
		OVector2    oVec2Result    = default;
		OVector3Int oVec3IntResult = default;
		OVector2Int oVec2IntResult = default;
		OQuaternion oQuatResult    = default;
		OColor32    oColor32Result = default;
		OColor      oColorResult   = default;
		OBounds     oBoundsResult  = default;
		var       oFloatResult   = 0.0f;

		for (var k = 0; k < 5; k++) // Warmup
		{
			var uVec4    = new UVector4(5, 20, 3, 17);
			var uVec3    = new UVector3(5, 20, 3);
			var uVec2    = new UVector2(5, 20);
			var uVec3Int = new UVector3Int(5, 20, 3);
			var uVec2Int = new UVector2Int(5, 20);
			var uColor32 = new UColor32(5, 20, 3, 17);
			UQuaternion uQuat    = UQuaternion.identity;
			var uBounds  = new UBounds(UVector3.one, UVector3.one);

			var oVec4    = new OVector4 { x      = 5, y   = 20, z = 3, w = 17 };
			var oVec3    = new OVector3 { x      = 5, y   = 20, z = 3 };
			var oVec2    = new OVector2 { x      = 5, y   = 20 };
			var oVec3Int = new OVector3Int { m_X = 5, m_Y = 20, m_Z = 3 };
			var oVec2Int = new OVector2Int { m_X = 5, m_Y = 20 };
			var oColor32 = new OColor32 { r      = 5, g   = 20, b = 3, a = 17 };
			var oQuat    = new OQuaternion { w   = 1f };
			var oBounds = new OBounds
			              {
				              m_Center  = new OVector3 { x = 1, y    = 1, z    = 1 },
				              m_Extents = new OVector3 { x = 0.5f, y = 0.5f, z = 0.5f }
			              };

			const float f  = 0.3f;
			const int   j  = 3;
			var         sw = new Stopwatch();

			// 1. UNITY TEST
			sw.Restart();
			switch (method)
			{
				case Methods.Vec4Plus:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = uVec4 + uVec4;
					break;
				case Methods.Vec4Minus:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = uVec4 - uVec4;
					break;
				case Methods.Vec4Multiply1:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = uVec4 * f;
					break;
				case Methods.Vec4Multiply2:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = f * uVec4;
					break;
				case Methods.Vec4UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = -uVec4;
					break;
				case Methods.Vec4Division:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = uVec4 / f;
					break;
				case Methods.Vec4Magnitude:
				case Methods.Vec4MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = uVec4.magnitude;
					break;
				case Methods.Vec4Normalize:
					for (var i = 0; i < OperationsNumber; i++) uVec4.Normalize();
					break;
				case Methods.Vec4NormalizeStatic:
				case Methods.Vec4NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = uVec4.normalized;
					break;
				case Methods.Vec4Distance:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = UVector4.Distance(uVec4, uVec4);
					break;
				case Methods.Vec4Scale:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = UVector4.Scale(uVec4, uVec4);
					break;
				case Methods.Vec4LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = UVector4.LerpUnclamped(uVec4, uVec4, f);
					break;
				case Methods.Vec4Lerp:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = UVector4.Lerp(uVec4, uVec4, f);
					break;
				case Methods.Vec4Min:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = UVector4.Min(uVec4, uVec4);
					break;
				case Methods.Vec4Max:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = UVector4.Max(uVec4, uVec4);
					break;
				case Methods.Vec3Plus:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = uVec3 + uVec3;
					break;
				case Methods.Vec3Minus:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = uVec3 - uVec3;
					break;
				case Methods.Vec3Multiply1:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = uVec3 * f;
					break;
				case Methods.Vec3Multiply2:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = f * uVec3;
					break;
				case Methods.Vec3UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = -uVec3;
					break;
				case Methods.Vec3Division:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = uVec3 / f;
					break;
				case Methods.Vec3Magnitude:
				case Methods.Vec3MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = uVec3.magnitude;
					break;
				case Methods.Vec3Normalize:
					for (var i = 0; i < OperationsNumber; i++) uVec3.Normalize();
					break;
				case Methods.Vec3NormalizeStatic:
				case Methods.Vec3NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = uVec3.normalized;
					break;
				case Methods.Vec3Distance:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = UVector3.Distance(uVec3, uVec3);
					break;
				case Methods.Vec3LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = UVector3.LerpUnclamped(uVec3, uVec3, f);
					break;
				case Methods.Vec3Lerp:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = UVector3.Lerp(uVec3, uVec3, f);
					break;
				case Methods.Vec3Cross:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = UVector3.Cross(uVec3, uVec3);
					break;
				case Methods.Vec3Scale:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = UVector3.Scale(uVec3, uVec3);
					break;
				case Methods.Vec3Min:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = UVector3.Min(uVec3, uVec3);
					break;
				case Methods.Vec3Max:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = UVector3.Max(uVec3, uVec3);
					break;
				case Methods.Vec2Plus:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = uVec2 + uVec2;
					break;
				case Methods.Vec2Minus:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = uVec2 - uVec2;
					break;
				case Methods.Vec2Multiply1:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = uVec2 * f;
					break;
				case Methods.Vec2Multiply2:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = f * uVec2;
					break;
				case Methods.Vec2Multiply3:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = UVector2.Scale(uVec2, uVec2);
					break;
				case Methods.Vec2UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = -uVec2;
					break;
				case Methods.Vec2Division1:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = uVec2 / f;
					break;
				case Methods.Vec2Division2:
					for (var i = 0; i < OperationsNumber; i++)
						uVec2Result = new UVector2(uVec2.x / uVec2.x, uVec2.y / uVec2.y);
					break;
				case Methods.Vec2MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = uVec2.magnitude;
					break;
				case Methods.Vec2Normalize:
					for (var i = 0; i < OperationsNumber; i++) uVec2.Normalize();
					break;
				case Methods.Vec2NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = uVec2.normalized;
					break;
				case Methods.Vec2Perpendicular:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = UVector2.Perpendicular(uVec2);
					break;
				case Methods.Vec2Distance:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = UVector2.Distance(uVec2, uVec2);
					break;
				case Methods.Vec2LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = UVector2.LerpUnclamped(uVec2, uVec2, f);
					break;
				case Methods.Vec2Lerp:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = UVector2.Lerp(uVec2, uVec2, f);
					break;
				case Methods.Vec2Scale:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = UVector2.Scale(uVec2, uVec2);
					break;
				case Methods.Vec2Min:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = UVector2.Min(uVec2, uVec2);
					break;
				case Methods.Vec2Max:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = UVector2.Max(uVec2, uVec2);
					break;
				case Methods.Vec3IntPlus:
					for (var i = 0; i < OperationsNumber; i++) uVec3IntResult = uVec3Int + uVec3Int;
					break;
				case Methods.Vec3IntMinus:
					for (var i = 0; i < OperationsNumber; i++) uVec3IntResult = uVec3Int - uVec3Int;
					break;
				case Methods.Vec3IntMultiply1:
					for (var i = 0; i < OperationsNumber; i++) uVec3IntResult = UVector3Int.Scale(uVec3Int, uVec3Int);
					break;
				case Methods.Vec3IntMultiply2:
					for (var i = 0; i < OperationsNumber; i++) uVec3IntResult = uVec3Int * j;
					break;
				case Methods.Vec3IntMagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = uVec3Int.magnitude;
					break;
				case Methods.Vec3IntDistance:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = UVector3Int.Distance(uVec3Int, uVec3Int);
					break;
				case Methods.Vec3IntScale:
					for (var i = 0; i < OperationsNumber; i++) uVec3IntResult = UVector3Int.Scale(uVec3Int, uVec3Int);
					break;
				case Methods.Vec3IntMin:
					for (var i = 0; i < OperationsNumber; i++) uVec3IntResult = UVector3Int.Min(uVec3Int, uVec3Int);
					break;
				case Methods.Vec3IntMax:
					for (var i = 0; i < OperationsNumber; i++) uVec3IntResult = UVector3Int.Max(uVec3Int, uVec3Int);
					break;
				case Methods.Vec2IntPlus:
					for (var i = 0; i < OperationsNumber; i++) uVec2IntResult = uVec2Int + uVec2Int;
					break;
				case Methods.Vec2IntMinus:
					for (var i = 0; i < OperationsNumber; i++) uVec2IntResult = uVec2Int - uVec2Int;
					break;
				case Methods.Vec2IntMultiply1:
					for (var i = 0; i < OperationsNumber; i++) uVec2IntResult = UVector2Int.Scale(uVec2Int, uVec2Int);
					break;
				case Methods.Vec2IntMultiply2:
					for (var i = 0; i < OperationsNumber; i++) uVec2IntResult = uVec2Int * j;
					break;
				case Methods.Vec2IntMagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = uVec2Int.magnitude;
					break;
				case Methods.Vec2IntDistance:
					for (var i = 0; i < OperationsNumber; i++) uFloatResult = UVector2Int.Distance(uVec2Int, uVec2Int);
					break;
				case Methods.Vec2IntScale:
					for (var i = 0; i < OperationsNumber; i++) uVec2IntResult = UVector2Int.Scale(uVec2Int, uVec2Int);
					break;
				case Methods.Vec2IntMin:
					for (var i = 0; i < OperationsNumber; i++) uVec2IntResult = UVector2Int.Min(uVec2Int, uVec2Int);
					break;
				case Methods.Vec2IntMax:
					for (var i = 0; i < OperationsNumber; i++) uVec2IntResult = UVector2Int.Max(uVec2Int, uVec2Int);
					break;
				case Methods.QuaternionMultiply:
					for (var i = 0; i < OperationsNumber; i++) uQuatResult = uQuat * uQuat;
					break;
				case Methods.Color32LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++)
						uColor32Result = UColor32.LerpUnclamped(uColor32, uColor32, f);
					break;
				case Methods.Color32Lerp:
					for (var i = 0; i < OperationsNumber; i++) uColor32Result = UColor32.Lerp(uColor32, uColor32, f);
					break;
				case Methods.ImplicitV2V3:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = uVec2;
					break;
				case Methods.ImplicitV2V4:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = uVec2;
					break;
				case Methods.ImplicitV3V2:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = uVec3;
					break;
				case Methods.ImplicitV3V4:
					for (var i = 0; i < OperationsNumber; i++) uVec4Result = uVec3;
					break;
				case Methods.ImplicitV4V2:
					for (var i = 0; i < OperationsNumber; i++) uVec2Result = uVec4;
					break;
				case Methods.ImplicitV4V3:
					for (var i = 0; i < OperationsNumber; i++) uVec3Result = uVec4;
					break;
				case Methods.ImplicitC32C:
					for (var i = 0; i < OperationsNumber; i++) uColorResult = uColor32;
					break;
				case Methods.ImplicitCC32:
					for (var i = 0; i < OperationsNumber; i++) uColor32Result = UColor.magenta;
					break;
				case Methods.BoundsExpandV:
					for (var i = 0; i < OperationsNumber; i++) uBounds.Expand(UVector3.one);
					uBoundsResult = uBounds;
					break;
				case Methods.BoundsExpandF:
					for (var i = 0; i < OperationsNumber; i++) uBounds.Expand(1f);
					uBoundsResult = uBounds;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(method), method, null);
			}

			sw.Stop();
			lastUnityTime = (float)sw.ElapsedTicks / TimeSpan.TicksPerMillisecond;

			// 2. OPTIMIZED TEST
			sw.Restart();
			switch (method)
			{
				case Methods.Vec4Plus:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = oVec4 + oVec4;
					break;
				case Methods.Vec4Minus:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = oVec4 - oVec4;
					break;
				case Methods.Vec4Multiply1:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = oVec4 * f;
					break;
				case Methods.Vec4Multiply2:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = f * oVec4;
					break;
				case Methods.Vec4UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = -oVec4;
					break;
				case Methods.Vec4Division:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = oVec4 / f;
					break;
				case Methods.Vec4Magnitude:
				case Methods.Vec4MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++)
						oFloatResult = MathF.Sqrt(oVec4.x * oVec4.x + oVec4.y * oVec4.y + oVec4.z * oVec4.z +
						                          oVec4.w * oVec4.w);
					break;
				case Methods.Vec4Normalize:
					for (var i = 0; i < OperationsNumber; i++)
					{
						var mag = MathF.Sqrt(oVec4.x * oVec4.x + oVec4.y * oVec4.y + oVec4.z * oVec4.z +
						                     oVec4.w * oVec4.w);
						if (!(mag > 0.00001f)) continue;
						var inverse = 1f / mag;
						oVec4.x *= inverse;
						oVec4.y *= inverse;
						oVec4.z *= inverse;
						oVec4.w *= inverse;
					}

					break;
				case Methods.Vec4NormalizeStatic:
				case Methods.Vec4NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++)
					{
						var mag = MathF.Sqrt(oVec4.x * oVec4.x + oVec4.y * oVec4.y + oVec4.z * oVec4.z +
						                     oVec4.w * oVec4.w);
						oVec4Result = (mag > 0.00001f)
							              ? new OVector4
							                {
								                x = oVec4.x / mag, y = oVec4.y / mag, z = oVec4.z / mag,
								                w = oVec4.w / mag
							                }
							              : default;
					}

					break;
				case Methods.Vec4Distance:
					for (var i = 0; i < OperationsNumber; i++) oFloatResult = OVector4.Distance(oVec4, oVec4);
					break;
				case Methods.Vec4Scale:
					for (var i = 0; i < OperationsNumber; i++)
						oVec4Result = new OVector4
						              {
							              x = oVec4.x * oVec4.x, y = oVec4.y * oVec4.y, z = oVec4.z * oVec4.z,
							              w = oVec4.w * oVec4.w
						              };
					break;
				case Methods.Vec4LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++)
						oVec4Result = new OVector4
						              {
							              x = oVec4.x + (oVec4.x - oVec4.x) * f, y = oVec4.y + (oVec4.y - oVec4.y) * f,
							              z = oVec4.z + (oVec4.z - oVec4.z) * f, w = oVec4.w + (oVec4.w - oVec4.w) * f
						              };
					break;
				case Methods.Vec4Lerp:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = OVector4.Lerp(oVec4, oVec4, f);
					break;
				case Methods.Vec4Min:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = OVector4.Min(oVec4, oVec4);
					break;
				case Methods.Vec4Max:
					for (var i = 0; i < OperationsNumber; i++) oVec4Result = OVector4.Max(oVec4, oVec4);
					break;
				case Methods.Vec3Plus:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = oVec3 + oVec3;
					break;
				case Methods.Vec3Minus:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = oVec3 - oVec3;
					break;
				case Methods.Vec3Multiply1:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = oVec3 * f;
					break;
				case Methods.Vec3Multiply2:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = f * oVec3;
					break;
				case Methods.Vec3UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = -oVec3;
					break;
				case Methods.Vec3Division:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = oVec3 / f;
					break;
				case Methods.Vec3Magnitude:
				case Methods.Vec3MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++) oFloatResult = OVector3.Magnitude(oVec3);
					break;
				case Methods.Vec3Normalize:
					for (var i = 0; i < OperationsNumber; i++) oVec3 = OVector3.Normalize(oVec3);
					break;
				case Methods.Vec3NormalizeStatic:
				case Methods.Vec3NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = OVector3.Normalize(oVec3);
					break;
				case Methods.Vec3Distance:
					for (var i = 0; i < OperationsNumber; i++) oFloatResult = OVector3.Distance(oVec3, oVec3);
					break;
				case Methods.Vec3LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++)
						oVec3Result = new OVector3
						              {
							              x = oVec3.x + (oVec3.x - oVec3.x) * f, y = oVec3.y + (oVec3.y - oVec3.y) * f,
							              z = oVec3.z + (oVec3.z - oVec3.z) * f
						              };
					break;
				case Methods.Vec3Lerp:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = OVector3.Lerp(oVec3, oVec3, f);
					break;
				case Methods.Vec3Cross:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = OVector3.Cross(oVec3, oVec3);
					break;
				case Methods.Vec3Scale:
					for (var i = 0; i < OperationsNumber; i++)
						oVec3Result = new OVector3
						              { x = oVec3.x * oVec3.x, y = oVec3.y * oVec3.y, z = oVec3.z * oVec3.z };
					break;
				case Methods.Vec3Min:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = OVector3.Min(oVec3, oVec3);
					break;
				case Methods.Vec3Max:
					for (var i = 0; i < OperationsNumber; i++) oVec3Result = OVector3.Max(oVec3, oVec3);
					break;
				case Methods.Vec2Plus:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = oVec2 + oVec2;
					break;
				case Methods.Vec2Minus:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = oVec2 - oVec2;
					break;
				case Methods.Vec2Multiply1:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = oVec2 * f;
					break;
				case Methods.Vec2Multiply2:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = f * oVec2;
					break;
				case Methods.Vec2Multiply3:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = oVec2 * oVec2;
					break;
				case Methods.Vec2UnaryNegation:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = -oVec2;
					break;
				case Methods.Vec2Division1:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = oVec2 / f;
					break;
				case Methods.Vec2Division2:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = oVec2 / oVec2;
					break;
				case Methods.Vec2MagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++)
						oFloatResult = MathF.Sqrt(oVec2.x * oVec2.x + oVec2.y * oVec2.y);
					break;
				case Methods.Vec2Normalize:
					for (var i = 0; i < OperationsNumber; i++)
					{
						var mag = MathF.Sqrt(oVec2.x * oVec2.x + oVec2.y * oVec2.y);
						if (!(mag > 0.00001f)) continue;
						var inverse = 1f / mag;
						oVec2.x *= inverse;
						oVec2.y *= inverse;
					}

					break;
				case Methods.Vec2NormalizedProp:
					for (var i = 0; i < OperationsNumber; i++)
					{
						var mag = MathF.Sqrt(oVec2.x * oVec2.x + oVec2.y * oVec2.y);
						oVec2Result = (mag > 0.00001f)
							              ? new OVector2 { x = oVec2.x / mag, y = oVec2.y / mag }
							              : default;
					}

					break;
				case Methods.Vec2Perpendicular:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = new OVector2 { x = -oVec2.y, y = oVec2.x };
					break;
				case Methods.Vec2Distance:
					for (var i = 0; i < OperationsNumber; i++) oFloatResult = OVector2.Distance(oVec2, oVec2);
					break;
				case Methods.Vec2LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++)
						oVec2Result = new OVector2
						              { x = oVec2.x + (oVec2.x - oVec2.x) * f, y = oVec2.y + (oVec2.y - oVec2.y) * f };
					break;
				case Methods.Vec2Lerp:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = OVector2.Lerp(oVec2, oVec2, f);
					break;
				case Methods.Vec2Scale:
					for (var i = 0; i < OperationsNumber; i++)
						oVec2Result = new OVector2 { x = oVec2.x * oVec2.x, y = oVec2.y * oVec2.y };
					break;
				case Methods.Vec2Min:
					for (var i = 0; i < OperationsNumber; i++)
						oVec2Result = new OVector2 { x = MathF.Min(oVec2.x, oVec2.x), y = MathF.Min(oVec2.y, oVec2.y) };
					break;
				case Methods.Vec2Max:
					for (var i = 0; i < OperationsNumber; i++)
						oVec2Result = new OVector2 { x = MathF.Max(oVec2.x, oVec2.x), y = MathF.Max(oVec2.y, oVec2.y) };
					break;
				case Methods.Vec3IntPlus:
					for (var i = 0; i < OperationsNumber; i++) oVec3IntResult = oVec3Int + oVec3Int;
					break;
				case Methods.Vec3IntMinus:
					for (var i = 0; i < OperationsNumber; i++) oVec3IntResult = oVec3Int - oVec3Int;
					break;
				case Methods.Vec3IntMultiply1:
					for (var i = 0; i < OperationsNumber; i++)
						oVec3IntResult = new OVector3Int
						                 {
							                 m_X = oVec3Int.m_X * oVec3Int.m_X, m_Y = oVec3Int.m_Y * oVec3Int.m_Y,
							                 m_Z = oVec3Int.m_Z * oVec3Int.m_Z
						                 };
					break;
				case Methods.Vec3IntMultiply2:
					for (var i = 0; i < OperationsNumber; i++) oVec3IntResult = oVec3Int * j;
					break;
				case Methods.Vec3IntMagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++)
						oFloatResult = MathF.Sqrt(oVec3Int.m_X * oVec3Int.m_X + oVec3Int.m_Y * oVec3Int.m_Y +
						                          oVec3Int.m_Z * oVec3Int.m_Z);
					break;
				case Methods.Vec3IntDistance:
					for (var i = 0; i < OperationsNumber; i++) oFloatResult = OVector3Int.Distance(oVec3Int, oVec3Int);
					break;
				case Methods.Vec3IntScale:
					for (var i = 0; i < OperationsNumber; i++)
						oVec3IntResult = new OVector3Int
						                 {
							                 m_X = oVec3Int.m_X * oVec3Int.m_X, m_Y = oVec3Int.m_Y * oVec3Int.m_Y,
							                 m_Z = oVec3Int.m_Z * oVec3Int.m_Z
						                 };
					break;
				case Methods.Vec3IntMin:
					for (var i = 0; i < OperationsNumber; i++) oVec3IntResult = OVector3Int.Min(oVec3Int, oVec3Int);
					break;
				case Methods.Vec3IntMax:
					for (var i = 0; i < OperationsNumber; i++)
						oVec3IntResult = new OVector3Int
						                 {
							                 m_X = Math.Max(oVec3Int.m_X, oVec3Int.m_X),
							                 m_Y = Math.Max(oVec3Int.m_Y, oVec3Int.m_Y),
							                 m_Z = Math.Max(oVec3Int.m_Z, oVec3Int.m_Z)
						                 };
					break;
				case Methods.Vec2IntPlus:
					for (var i = 0; i < OperationsNumber; i++) oVec2IntResult = oVec2Int + oVec2Int;
					break;
				case Methods.Vec2IntMinus:
					for (var i = 0; i < OperationsNumber; i++) oVec2IntResult = oVec2Int - oVec2Int;
					break;
				case Methods.Vec2IntMultiply1:
					for (var i = 0; i < OperationsNumber; i++)
						oVec2IntResult = new OVector2Int
						                 { m_X = oVec2Int.m_X * oVec2Int.m_X, m_Y = oVec2Int.m_Y * oVec2Int.m_Y };
					break;
				case Methods.Vec2IntMultiply2:
					for (var i = 0; i < OperationsNumber; i++) oVec2IntResult = oVec2Int * j;
					break;
				case Methods.Vec2IntMagnitudeProp:
					for (var i = 0; i < OperationsNumber; i++)
						oFloatResult = MathF.Sqrt(oVec2Int.m_X * oVec2Int.m_X + oVec2Int.m_Y * oVec2Int.m_Y);
					break;
				case Methods.Vec2IntDistance:
					for (var i = 0; i < OperationsNumber; i++) oFloatResult = OVector2Int.Distance(oVec2Int, oVec2Int);
					break;
				case Methods.Vec2IntScale:
					for (var i = 0; i < OperationsNumber; i++)
						oVec2IntResult = new OVector2Int
						                 { m_X = oVec2Int.m_X * oVec2Int.m_X, m_Y = oVec2Int.m_Y * oVec2Int.m_Y };
					break;
				case Methods.Vec2IntMin:
					for (var i = 0; i < OperationsNumber; i++) oVec2IntResult = OVector2Int.Min(oVec2Int, oVec2Int);
					break;
				case Methods.Vec2IntMax:
					for (var i = 0; i < OperationsNumber; i++) oVec2IntResult = OVector2Int.Max(oVec2Int, oVec2Int);
					break;
				case Methods.QuaternionMultiply:
					for (var i = 0; i < OperationsNumber; i++) oQuatResult = oQuat * oQuat;
					break;
				case Methods.Color32LerpUnclamped:
					for (var i = 0; i < OperationsNumber; i++)
						oColor32Result = OColor32.LerpUnclamped(oColor32, oColor32, f);
					break;
				case Methods.Color32Lerp:
					for (var i = 0; i < OperationsNumber; i++) oColor32Result = OColor32.Lerp(oColor32, oColor32, f);
					break;
				case Methods.ImplicitV2V3:
					for (var i = 0; i < OperationsNumber; i++)
						oVec3Result = new OVector3 { x = oVec2.x, y = oVec2.y, z = 0f };
					break;
				case Methods.ImplicitV2V4:
					for (var i = 0; i < OperationsNumber; i++)
						oVec4Result = new OVector4 { x = oVec2.x, y = oVec2.y, z = 0f, w = 0f };
					break;
				case Methods.ImplicitV3V2:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = new OVector2 { x = oVec3.x, y = oVec3.y };
					break;
				case Methods.ImplicitV3V4:
					for (var i = 0; i < OperationsNumber; i++)
						oVec4Result = new OVector4 { x = oVec3.x, y = oVec3.y, z = oVec3.z, w = 0f };
					break;
				case Methods.ImplicitV4V2:
					for (var i = 0; i < OperationsNumber; i++) oVec2Result = new OVector2 { x = oVec4.x, y = oVec4.y };
					break;
				case Methods.ImplicitV4V3:
					for (var i = 0; i < OperationsNumber; i++)
						oVec3Result = new OVector3 { x = oVec4.x, y = oVec4.y, z = oVec4.z };
					break;
				case Methods.ImplicitC32C:
					for (var i = 0; i < OperationsNumber; i++) oColorResult = oColor32;
					break;
				case Methods.ImplicitCC32:
					for (var i = 0; i < OperationsNumber; i++)
						oColor32Result = new OColor { r = 1f, g = 0f, b = 1f, a = 1f };
					break;
				case Methods.BoundsExpandV:
					for (var i = 0; i < OperationsNumber; i++) oBounds.Expand(new OVector3 { x = 1, y = 1, z = 1 });
					oBoundsResult = oBounds;
					break;
				case Methods.BoundsExpandF:
					for (var i = 0; i < OperationsNumber; i++) oBounds.Expand(1f);
					oBoundsResult = oBounds;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(method), method, null);
			}

			sw.Stop();
			lastOptTime = (float)sw.ElapsedTicks / TimeSpan.TicksPerMillisecond;
		}

		var uResStr = "";
		var oResStr = "";

		NumberFormatInfo ni = CultureInfo.InvariantCulture.NumberFormat;

		var m = method.ToString();
		if (m.StartsWith("Vec4"))
		{
			uResStr = uVec4Result.ToString("F2", ni);
			oResStr = string.Format(inv, "({0:F2}, {1:F2}, {2:F2}, {3:F2})", oVec4Result.x, oVec4Result.y,
			                        oVec4Result.z, oVec4Result.w);
			if (m.Contains("Magnitude") || m.Contains("Distance"))
			{
				uResStr = uFloatResult.ToString("F5", ni);
				oResStr = oFloatResult.ToString("F5", ni);
			}
		}
		else if (m.StartsWith("Vec3Int"))
		{
			uResStr = uVec3IntResult.ToString(null, ni);
			oResStr = string.Format(inv, "({0}, {1}, {2})", oVec3IntResult.m_X, oVec3IntResult.m_Y, oVec3IntResult.m_Z);
			if (m.Contains("Magnitude") || m.Contains("Distance"))
			{
				uResStr = uFloatResult.ToString("F5", ni);
				oResStr = oFloatResult.ToString("F5", ni);
			}
		}
		else if (m.StartsWith("Vec3"))
		{
			uResStr = uVec3Result.ToString("F2", ni);
			oResStr = string.Format(inv, "({0:F2}, {1:F2}, {2:F2})", oVec3Result.x, oVec3Result.y, oVec3Result.z);
			if (m.Contains("Magnitude") || m.Contains("Distance"))
			{
				uResStr = uFloatResult.ToString("F5", ni);
				oResStr = oFloatResult.ToString("F5", ni);
			}
		}
		else if (m.StartsWith("Vec2Int"))
		{
			uResStr = uVec2IntResult.ToString(null, ni);
			oResStr = string.Format(inv, "({0}, {1})", oVec2IntResult.m_X, oVec2IntResult.m_Y);
			if (m.Contains("Magnitude") || m.Contains("Distance"))
			{
				uResStr = uFloatResult.ToString("F5", ni);
				oResStr = oFloatResult.ToString("F5", ni);
			}
		}
		else if (m.StartsWith("Vec2"))
		{
			uResStr = uVec2Result.ToString("F2", ni);
			oResStr = string.Format(inv, "({0:F2}, {1:F2})", oVec2Result.x, oVec2Result.y);
			if (m.Contains("Magnitude") || m.Contains("Distance"))
			{
				uResStr = uFloatResult.ToString("F5", ni);
				oResStr = oFloatResult.ToString("F5", ni);
			}
		}
		else if (m.StartsWith("Quaternion"))
		{
			uResStr = uQuatResult.ToString("F5", ni);
			oResStr = string.Format(inv, "({0:F5}, {1:F5}, {2:F5}, {3:F5})", oQuatResult.x, oQuatResult.y,
			                        oQuatResult.z, oQuatResult.w);
		}
		else if (m.StartsWith("Color32") || m == "ImplicitCC32")
		{
			uResStr = uColor32Result.ToString(null, ni);
			oResStr = string.Format(inv, "RGBA({0}, {1}, {2}, {3})", oColor32Result.r, oColor32Result.g,
			                        oColor32Result.b, oColor32Result.a);
		}
		else if (m == "ImplicitC32C")
		{
			uResStr = uColorResult.ToString("F3", ni);
			oResStr = string.Format(inv, "RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", oColorResult.r, oColorResult.g,
			                        oColorResult.b, oColorResult.a);
		}
		else if (m.StartsWith("Bounds"))
		{
			uResStr = uBoundsResult.ToString("F2", ni);
			oResStr = string.Format(inv, "Center: ({0:F2}, {1:F2}, {2:F2}), Extents: ({3:F2}, {4:F2}, {5:F2})",
			                        oBoundsResult.m_Center.x, oBoundsResult.m_Center.y, oBoundsResult.m_Center.z,
			                        oBoundsResult.m_Extents.x, oBoundsResult.m_Extents.y, oBoundsResult.m_Extents.z);
		}
		else
		{
			switch (m)
			{
				case "ImplicitV2V3":
				case "ImplicitV4V3":
					uResStr = uVec3Result.ToString("F2", ni);
					oResStr = string.Format(inv, "({0:F2}, {1:F2}, {2:F2})", oVec3Result.x, oVec3Result.y,
					                        oVec3Result.z);
					break;
				case "ImplicitV2V4":
				case "ImplicitV3V4":
					uResStr = uVec4Result.ToString("F2", ni);
					oResStr = string.Format(inv, "({0:F2}, {1:F2}, {2:F2}, {3:F2})", oVec4Result.x, oVec4Result.y,
					                        oVec4Result.z, oVec4Result.w);
					break;
				case "ImplicitV3V2":
				case "ImplicitV4V2":
					uResStr = uVec2Result.ToString("F2", ni);
					oResStr = string.Format(inv, "({0:F2}, {1:F2})", oVec2Result.x, oVec2Result.y);
					break;
			}
		}

		// Output logic
		var speedMultiplier = lastUnityTime / Mathf.Max(lastOptTime, 0.01f);
		csvStats += string.Format(inv, "{0},{1:F2},{2:F2},{3:F2}x\n", method, lastUnityTime, lastOptTime,
		                          speedMultiplier);

		var isMatch = (uResStr == oResStr);
		textResults += $"--- {method} ---\n";
		textResults += string.Format(inv, "Time:      Unity {0:F2}ms   | Optimized {1:F2}ms   [{2:F2}x FASTER]\n",
		                             lastUnityTime, lastOptTime, speedMultiplier);
		textResults += "Result:\n";
		textResults += $"  Unity:   {uResStr}\n";
		textResults += $"  Optim:   {oResStr}\n";
		textResults += $"  Match:   {(isMatch ? "YES" : "NO")}\n";
		textResults += "========================================\n\n";

		// Next method test or finish and save
		var maxEnumValue = (Methods)Enum.GetValues(typeof(Methods)).Cast<int>().Max();
		if (method < maxEnumValue)
		{
			yield return new WaitForEndOfFrame();
			StartCoroutine(RunTestsOnMethod(method + 1));
		}
		else
		{
#if UNITY_EDITOR
			var folderPath = "./Assets";
#else
            string folderPath = ".";
#endif
			var statsFilePath = Path.Combine(folderPath, "Comparison_Stats.csv");
			File.WriteAllText(statsFilePath, csvStats);
			Debug.Log("Stats saved as " + statsFilePath);

			var resultsFilePath = Path.Combine(folderPath, "Comparison_Results.txt");
			File.WriteAllText(resultsFilePath, textResults);
			Debug.Log("Results saved as " + resultsFilePath);

			Application.Quit();
		}
	}
}

public enum Methods
{
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
	Vec3IntPlus,
	Vec3IntMinus,
	Vec3IntMultiply1,
	Vec3IntMultiply2,
	Vec3IntMagnitudeProp,
	Vec3IntDistance,
	Vec3IntScale,
	Vec3IntMin,
	Vec3IntMax,
	Vec2IntPlus,
	Vec2IntMinus,
	Vec2IntMultiply1,
	Vec2IntMultiply2,
	Vec2IntMagnitudeProp,
	Vec2IntDistance,
	Vec2IntScale,
	Vec2IntMin,
	Vec2IntMax,
	QuaternionMultiply,
	Color32LerpUnclamped,
	Color32Lerp,
	ImplicitV2V3,
	ImplicitV2V4,
	ImplicitV3V2,
	ImplicitV3V4,
	ImplicitV4V2,
	ImplicitV4V3,
	ImplicitC32C,
	ImplicitCC32,
	BoundsExpandV,
	BoundsExpandF
}