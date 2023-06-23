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
		PlayerHealth player = Container
			.InstantiatePrefabForComponent<PlayerHealth>
			(playerPrefab, spawnPlayerPoint.position, Quaternion.identity, null);

		CharacterController2D characterController = player.GetComponent<CharacterController2D> ();

		Container
			.Bind<PlayerHealth>()
			.FromInstance(player)
			.AsSingle();
		Container
			.Bind<CharacterController2D>()
			.FromInstance(characterController)
			.AsSingle();
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