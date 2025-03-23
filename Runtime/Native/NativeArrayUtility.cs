using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace TextTween.Native {
    public static class NativeArrayUtility {
        public static unsafe void MemCpy(this int[] source, NativeArray<int> destination) {
            fixed (void* managedArrayPointer = source) {
                UnsafeUtility.MemCpy (
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(destination),
                    managedArrayPointer,
                    source.Length * (long) UnsafeUtility.SizeOf<int>()
                );
            }
        }
        
        public static unsafe void MemCpy(this float[] source, NativeArray<float> destination) {
            fixed (void* managedArrayPointer = source) {
                UnsafeUtility.MemCpy (
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(destination),
                    managedArrayPointer,
                    source.Length * (long) UnsafeUtility.SizeOf<float>()
                );
            }
        }
        
        public static unsafe void MemCpy(this Vector2[] source, NativeArray<float2> destination) {
            fixed (void* managedArrayPointer = source) {
                UnsafeUtility.MemCpy (
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(destination),
                    managedArrayPointer,
                    source.Length * (long) UnsafeUtility.SizeOf<Vector2>()
                );
            }
        }
        
        public static unsafe void MemCpy(this Vector3[] source, NativeArray<float3> destination) {
            fixed (void* managedArrayPointer = source) {
                UnsafeUtility.MemCpy (
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(destination),
                    managedArrayPointer,
                    source.Length * (long) UnsafeUtility.SizeOf<Vector3>()
                );
            }
        }
        
        public static unsafe void MemCpy(this Vector4[] source, NativeArray<float4> destination) {
            fixed (void* managedArrayPointer = source) {
                UnsafeUtility.MemCpy (
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(destination),
                    managedArrayPointer,
                    source.Length * (long) UnsafeUtility.SizeOf<Vector4>()
                );
            }
        }
        
        public static unsafe void MemCpy(this Color[] source, NativeArray<float4> destination) {
            fixed (void* managedArrayPointer = source) {
                UnsafeUtility.MemCpy (
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(destination),
                    managedArrayPointer,
                    source.Length * (long) UnsafeUtility.SizeOf<Color>()
                );
            }
        }
        
        public static unsafe void MemCpy(this NativeArray<int> source, int[] destination) {
            fixed (void* managedArrayPointer = destination) {
                UnsafeUtility.MemCpy (
                    managedArrayPointer,
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(source),
                    destination.Length * (long) UnsafeUtility.SizeOf<int>()
                );
            }
        }
        
        public static unsafe void MemCpy(this NativeArray<float> source, float[] destination) {
            fixed (void* managedArrayPointer = destination) {
                UnsafeUtility.MemCpy (
                    managedArrayPointer,
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(source),
                    destination.Length * (long) UnsafeUtility.SizeOf<int>()
                );
            }
        }
        
        public static unsafe void MemCpy(this NativeArray<float2> source, Vector2[] destination) {
            fixed (void* managedArrayPointer = destination) {
                UnsafeUtility.MemCpy (
                    managedArrayPointer,
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(source),
                    destination.Length * (long) UnsafeUtility.SizeOf<Vector2>()
                );
            }
        }
        
        public static unsafe void MemCpy(this NativeArray<float3> source, Vector3[] destination) {
            fixed (void* managedArrayPointer = destination) {
                UnsafeUtility.MemCpy (
                    managedArrayPointer,
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(source),
                    destination.Length * (long) UnsafeUtility.SizeOf<Vector3>()
                );
            }
        }
        
        public static unsafe void MemCpy(this NativeArray<float4> source, Vector4[] destination) {
            fixed (void* managedArrayPointer = destination) {
                UnsafeUtility.MemCpy (
                    managedArrayPointer,
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(source),
                    destination.Length * (long) UnsafeUtility.SizeOf<Vector4>()
                );
            }
        }
        
        public static unsafe void MemCpy(this NativeArray<float4> source, Color[] destination) {
            fixed (void* managedArrayPointer = destination) {
                UnsafeUtility.MemCpy (
                    managedArrayPointer,
                    NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(source),
                    destination.Length * (long) UnsafeUtility.SizeOf<Color>()
                );
            }
        }
    }
}