using System.Linq;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor((typeof(LevelStaticData)))]
    public class LevelStateDataEditor : UnityEditor.Editor
    {
        private const string Initialpoint = "InitialPoint";
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarket>()
                    .Select(x =>
                        new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
                levelData.InitialHeroPosition = GameObject.FindWithTag(Initialpoint).transform.position;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}