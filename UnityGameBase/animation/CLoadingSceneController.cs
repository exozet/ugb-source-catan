using System;
using UnityEngine;
using System.Collections;

namespace UGB.Animation
{
	/// <summary>
	/// Controls Playback of an animated scene while loading another scene in sceneTransition. 
	/// 
	/// To use this you will have to assign an instance of this class to the Game::mSceneTransition::mLoadingScreenController member. 
	/// 
	/// It will expect a CLoadingScene component present in the loading scene with assigned animations. 
	/// </summary>


	public class CLoadingSceneController : MonoBehaviour, ILoadingScreenController
	{
		/// <summary>
		/// The name of the scene, which is loaded (additive) to the game on startup. This scene must contain a CLoadingScene Component. 
		/// </summary>
		public string mSceneName;
		bool initialized = false;

		CLoadingScene loadingScene;

		public void Initialize (Action doneCallback)
		{
			initialized = false;
			Application.LoadLevelAdditive(mSceneName);
			StartCoroutine(WaitForScene(doneCallback));
		}

		IEnumerator WaitForScene(System.Action doneCallback)
		{
			while(loadingScene == null)
			{
				loadingScene = GameObject.FindObjectOfType(typeof(CLoadingScene)) as CLoadingScene;
				if(loadingScene == null)
					yield return 0;
			}

			loadingScene.camera.enabled = false;
			initialized = true;

			if(doneCallback != null)
				doneCallback();
		}

		public void AnimateInBegin (Action doneCallback)
		{
			loadingScene.camera.enabled = true;
			var animation = loadingScene.GetComponent<UnityEngine.Animation>();
			animation.Play( loadingScene.inAnimation.name );
			StartCoroutine(WaitForEndOfAnimation(loadingScene.inAnimation.name, () => {
			
				animation.Play( loadingScene.loopAnimation.name );
				if(doneCallback != null)
					doneCallback();

			}));
		}

		public void AnimateOutBegin (Action doneCallback)
		{
			var animation = loadingScene.GetComponent<UnityEngine.Animation>();
			animation.Stop( loadingScene.loopAnimation.name );
			animation.Play(loadingScene.outAnimation.name);
			StartCoroutine(WaitForEndOfAnimation(loadingScene.outAnimation.name, () => {

				loadingScene.camera.enabled = false;

				if(doneCallback != null)
					doneCallback();

			}));
		}

		public bool IsInitialized {
			get {
				return initialized;
			}
		}

		public bool CanLoadAsync()
		{
			if(Application.HasProLicense())
			{
				if(SystemInfo.systemMemorySize < 1024)
					return false;

				return true;
			}
			return false;
		}


		IEnumerator WaitForEndOfAnimation(string animation, System.Action doneCallback)
		{

			while(loadingScene.GetComponent<UnityEngine.Animation>().IsPlaying(animation))
			{
				//Debug.Log("Waiting for Animation: " + pAnimation);
				yield return 0;
			}


			if(doneCallback != null)
				doneCallback();
		}

	}

}