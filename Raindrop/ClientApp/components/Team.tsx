import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as TeamStore from '../store/Team';

type TeamProps =
    TeamStore.TeamState
    & typeof TeamStore.actionCreators

export class Team extends React.Component<TeamProps, {}> {
    componentWillReceiveProps(nextProps: TeamProps) {
        if (!this.props.isEditing && nextProps.isEditing) {
            this.props.startEditing();
        }

        if (this.props.name !== nextProps.name) {
            this.props.updateName(nextProps.name)
        }
    }

    public render() {
        return <tr>
            <td>
                {this.renderName()}
            </td>
            <td><button onClick={this.props.startEditing()}><span className='glyphicon glyphicon-edit'></span> Edit</button></td>
        </tr>
    }

    private renderName() {
        return this.props.isEditing
            ? <input type="text" value={this.props.name} />
            : <span>{this.props.name}</span>
    }
}

export default connect(
    (state: TeamStore.TeamState) => state,     // Selects which state properties are merged into the component's props
    TeamStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Team) as typeof Team;
