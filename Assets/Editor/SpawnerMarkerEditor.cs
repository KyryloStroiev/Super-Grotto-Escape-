using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemy;
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

            Handles.Label(spawner.transform.position + Vector3.up * 0.7f + Vector3.left * 0.5f,
                spawner.MonsterTypeId.ToString());
            
            Vector3 lookDirection = spawner.IsLookLeft ? Vector3.left : Vector3.right;


            Vector3 arrowPosition = spawner.transform.position  + lookDirection * 0.5f;
            
            Handles.color = EnemyColor(spawner.MonsterTypeId);
            Handles.ArrowHandleCap(
                0,
                arrowPosition,
                Quaternion.LookRotation(lookDirection),
                1,
                EventType.Repaint
            );
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






