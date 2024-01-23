using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor((typeof(SpawnMarket)))]
    public class SpawnerMarkerEditor: UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarket spawner, GizmoType gizmo)
        {
            Gizmos.color = EnemyColor(spawner.MonsterTypeId);
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }

        private static Color EnemyColor(MonsterTypeId typeId)
        {
            switch (typeId)
            {
                case MonsterTypeId.Bat:
                    return Color.red;
                case MonsterTypeId.Lizard:
                    return Color.green;
                case MonsterTypeId.Skelet:
                    return Color.blue;
                default:
                   return Color.white;
            }
        }
    }
}