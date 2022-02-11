using System;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using Autofac;
using Redux;

namespace RSP.Game
{
    public class GameManager
    {        
        public Store<GameState> GameStore { get; private set; }

        public GameManager(IContainer container)
        {
            GameStore = new Store<GameState>(Reducer, new GameState(), new ActionEffectMiddleware(container).Middleware);
        }

        private GameState Reducer(GameState state, IAction action)
        {
            Console.WriteLine("reducer handling "+action.GetType());
            var genericActionReducerType = typeof(ActionReducer<,>).MakeGenericType(action.GetType(), typeof(GameState));
            var actionReducerType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => genericActionReducerType.IsAssignableFrom(t));
  
            var actionReducer = actionReducerType != null 
                ? Activator.CreateInstance(actionReducerType) as ActionReducer<GameState> 
                : null;

            return actionReducer == null 
                ? state 
                : actionReducer.Reducer(state, action);
        }
        
        
    }
}