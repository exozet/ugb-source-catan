using System;

namespace UGB.Core.DontUse
{
	
    public class LogicDummy : GameLogicImplementationBase
    {
		
		#region implemented abstract members of GameLogicImplementationBase


        public override void Update()
        {
            
        }

        public override bool OnBeforeRestart()
        {
            return true;
        }

        public override bool OnBeforePause()
        {
            return true;
        }

        public override void Start()
        {
            // register game states here
			
            // SGameState.Add(1,"MainMenu");
			
			
            // register languages; first registered is the default language. 
            // default language will be used if no suitable system language was detected and 
            // no language setting was found in playerprefs
			
            // Languages.Add(1,"de");
            // Languages.Add(2,"en");
        }

        public override void GameSetupReady()
        {
        }

        public override void GameStateChanged(SGameState pOldState, SGameState pCurrentGameState)
        {
        }

        public override SGameState GetCurrentGameState()
        {
            return 0;
        }
		#endregion
    }
}

