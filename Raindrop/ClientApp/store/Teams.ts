import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import { TeamState } from './Team';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeamsListState {
    isLoading: boolean;
    teams: TeamState[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface AddTeamAction {
    type: 'ADD_TEAM';
}

interface RequestTeamsAction {
    type: 'REQUEST_TEAMS';
}

interface ReceiveTeamsAction {
    type: 'RECEIVE_TEAMS';
    teams: TeamState[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = AddTeamAction | RequestTeamsAction | ReceiveTeamsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeams: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        let fetchTask = fetch(`api/Teams/`)
            .then(response => response.json() as Promise<TeamState[]>)
            .then(data => {
                dispatch({ type: 'RECEIVE_TEAMS', teams: data });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_TEAMS' });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeamsListState = { teams: [], isLoading: false };

export const reducer: Reducer<TeamsListState> = (state: TeamsListState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'ADD_TEAM':
            return {
                teams: state.teams.concat([{ name: 'New Team', imagePath: '', isEditing: true }]),
                isLoading: state.isLoading
            };
        case 'REQUEST_TEAMS':
            return {
                teams: state.teams,
                isLoading: true
            };
        case 'RECEIVE_TEAMS':
            return {
                teams: action.teams,
                isLoading: false
            };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
