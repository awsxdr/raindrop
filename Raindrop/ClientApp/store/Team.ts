import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeamState {
    name: string;
    imagePath: string;
    isEditing: boolean;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface UpdateNameAction {
    type: 'UPDATE_NAME';
    name: string;
}

interface StartEditingAction {
    type: 'START_EDITING';
}

interface StopEditingAction {
    type: 'STOP_EDITING';
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = UpdateNameAction | StartEditingAction | StopEditingAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    startEditing: (): AppThunkAction<StartEditingAction> => (dispatch, getState) => {
        console.log("Start editing");
        dispatch({ type: 'START_EDITING' });
    },
    acceptChanges: (): AppThunkAction<StopEditingAction> => (dispatch, getState) => {
        dispatch({ type: 'STOP_EDITING' });
    },
    revertChanges: (): AppThunkAction<StopEditingAction> => (dispatch, getState) => {
        dispatch({ type: 'STOP_EDITING' });
    },
    updateName: (name: string): AppThunkAction<UpdateNameAction> => (dispatch, getState) => {
        dispatch({ type: 'UPDATE_NAME', name: name });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeamState = { name: '', imagePath: '', isEditing: false };

export const reducer: Reducer<TeamState> = (state: TeamState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'START_EDITING':
            return {
                name: state.name,
                imagePath: state.imagePath,
                isEditing: true
            };
        case 'STOP_EDITING':
            return {
                name: state.name,
                imagePath: state.imagePath,
                isEditing: false
            };
        case 'UPDATE_NAME':
            return {
                name: action.name,
                imagePath: state.imagePath,
                isEditing: state.isEditing
            };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
