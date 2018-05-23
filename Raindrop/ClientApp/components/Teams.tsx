import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { Team } from './Team'
import { ApplicationState } from '../store';
import * as TeamsListState from '../store/Teams';
import * as TeamStore from '../store/Team';

// At runtime, Redux will merge together...
type TeamsProps =
    TeamsListState.TeamsListState
    & typeof TeamsListState.actionCreators
    & RouteComponentProps<{}>;

class Teams extends React.Component<TeamsProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestTeams();
    }

    public render() {
        return <div>
            <h1>Teams</h1>
            {this.renderTeamsTable()}
        </div>;
    }

    private renderTeamsTable() {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
                {this.props.teams.sort((a, b) => a.name < b.name ? -1 : a.name > b.name ? 1 : 0).map(team =>
                    <Team key={team.name} {...team} {...TeamStore.actionCreators} />
                )}
            </tbody>
        </table>;
    }
}

export default connect(
    (state: ApplicationState) => state.teams,     // Selects which state properties are merged into the component's props
    TeamsListState.actionCreators                 // Selects which action creators are merged into the component's props
)(Teams) as typeof Teams;
