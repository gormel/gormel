package controllers;
import java.util.ArrayList;
import java.util.List;
import java.util.Stack;

import states.State;


public class StateController {
	private static StateController inst = null;
	public static StateController getInstance() {
		if (inst == null)
			inst = new StateController();
		return inst;
	}
	
	private List<StateUpdateListener> listeners = new ArrayList<>();
	private Stack<State> stateStorage = new Stack<>();
	
	public State peek() {
		return stateStorage.peek();
	}
	
	public State pop() {
		State result = stateStorage.pop();
		onStateUpdate();
		return result;
	}
	
	public void push(State s) {
		stateStorage.add(s);
		onStateUpdate();
	}
	
	public void addUpdateListener(StateUpdateListener listener) {
		listeners.add(listener);
	}
	
	public void removeUpdateListener(StateUpdateListener listener) {
		listeners.remove(listener);
	}
	
	private void onStateUpdate() {
		List<StateUpdateListener> temp = new ArrayList<StateUpdateListener>();
		for (StateUpdateListener l : listeners)
			temp.add(l);
		for (StateUpdateListener l : temp)
			l.onStateUpdate();
	}
}
