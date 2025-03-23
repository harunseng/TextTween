using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace TextTween.Modifiers {
    [AddComponentMenu("TextTween/Modifiers/Transform Modifier")]
    public class TransformModifier : CharModifier {
        public enum Type {
            Position, 
            Rotation, 
            Scale
        }

        [Flags]
        public enum Scale {
            X = 1 << 0, 
            Y = 1 << 1, 
            Z = 1 << 2,
        }

        [SerializeField] private Type _type;
        [SerializeField] private Scale _scale;
        [SerializeField] private float3 _intensity;
        [SerializeField] private float2 _pivot;
        
        public override JobHandle Schedule(
            float4 bounds,
            float progress,
            NativeArray<float3> vertices,
            NativeArray<float4> colors,
            NativeArray<CharData> charData,
            JobHandle dependency) {
            return new Job(
                vertices, 
                charData, 
                _intensity,
                _pivot,
                _type,
                _scale,
                progress).Schedule(charData.Length, 64, dependency);
        }

        public override void Dispose() {}

        private struct Job : IJobParallelFor {
            [NativeDisableParallelForRestriction] private NativeArray<float3> _vertices;
            [ReadOnly] private NativeArray<CharData> _data;
            private readonly Type _type;
            private readonly Scale _scale;
            private readonly float3 _intensity;
            private readonly float2 _pivot;
            private readonly float _progress;

            public Job(
                NativeArray<float3> vertices, 
                NativeArray<CharData> data, 
                float3 intensity, 
                float2 pivot,
                Type type,
                Scale scale,
                float progress) {
                _vertices = vertices;
                _data = data;
                _type = type;
                _scale = scale;
                _intensity = intensity;
                _pivot = pivot;
                _progress = progress;
            }

            public void Execute(int index) {
                var characterData = _data[index];
                if (!characterData.IsVisible) return;
                var vertexOffset = characterData.VertexIndex;
                var offset = Offset(_vertices, vertexOffset, _pivot);
                var p = Remap(_progress, characterData.Interval);
                var m = GetTransformation(p);
                for (var i = 0; i < characterData.VertexCount; i++) {
                    _vertices[vertexOffset + i] -= offset;
                    _vertices[vertexOffset + i] = math.mul(m, new float4(_vertices[vertexOffset + i], 1)).xyz;
                    _vertices[vertexOffset + i] += offset;
                }
            }

            private float4x4 GetTransformation(float progress) {
                var e = progress;
                var fp = float3.zero;
                var fr = quaternion.identity;
                var fs = (float3) 1;
                switch (_type) {
                    case Type.Position:
                        fp = _intensity * e;
                        break;
                    case Type.Rotation:
                        fr = quaternion.Euler(math.radians(_intensity * e));
                        break;
                    case Type.Scale:
                        if (_scale.HasFlag(Scale.X)) fs.x = e * _intensity.x;
                        if (_scale.HasFlag(Scale.Y)) fs.y = e * _intensity.y;
                        if (_scale.HasFlag(Scale.Z)) fs.z = e * _intensity.z;
                        break;
                    default:
                        return float4x4.identity;
                }
                return float4x4.TRS(fp, fr, fs);
            }
        }
    }
}