using UnityEngine;
using Zenject;

public class SceneUnstaller : MonoInstaller
{
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private Transform spawnPlayerPoint;
	public override void InstallBindings()
	{
		
		BindGameManager();
		BindAudioManager();
		BindPlayer();
		BindEnemyLogic();
	}

	private void BindPlayer()
	{
		PlayerMovement playerMovement = Container
	   .InstantiatePrefabForComponent<PlayerMovement>(playerPrefab, spawnPlayerPoint.position, Quaternion.identity, null);

		Container
			.Bind<PlayerMovement>()
			.FromInstance(playerMovement)
			.AsSingle();

		PlayerHealth playerHealth = playerMovement.GetComponent<PlayerHealth>();
		Container
			.Bind<PlayerHealth>()
			.FromInstance(playerHealth)
			.AsTransient();
	}

	private void BindGameManager()
	{
		Container
			.Bind<GameManager>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
	
	}
	private void BindAudioManager()
	{
		Container
			.Bind<AudioManager>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
	}
	private void BindEnemyLogic()
	{
		Container
			.Bind<EnemyLogic>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
	}

}