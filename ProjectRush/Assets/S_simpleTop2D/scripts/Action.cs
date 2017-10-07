
using UnityEngine;

public enum ActionIs
{
	Sneaky, Walk, Run, Stop, Ring
};

public class Action : MonoBehaviour {

	public delegate void ActionFunction (int action);
	public event ActionFunction OnAction;

	private int _currentAction;

	public int CurrentAction
	{
		get
        {
            return _currentAction;
        }

		set
        { 
			_currentAction = value;

			if(OnAction != null)
				OnAction(_currentAction);
		}
	}
}