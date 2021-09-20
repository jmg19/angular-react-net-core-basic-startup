import React from 'react';
import { _IAppServicesFactory, IAppServicesFactory } from '../../AppServicesFactory';
import './Loading.css'
import loadingAnimationSrc from './loading-animation.svg';

interface LoadingStateData{}

interface LoadingPropsData{}

export class Loading extends React.Component<LoadingPropsData, LoadingStateData>  {
    private serviceFactory: IAppServicesFactory = _IAppServicesFactory();

    openMessagesPopup = () => {
    }

    componentDidMount(){        
    }

    componentWillUnmount(){        
    }

    render() {        
        return (
            <div className="loading">
                <div className="center">
                <img src={loadingAnimationSrc} alt="Loading..." />
                <div className="info-comunication-errors">Some problems happen ...&nbsp;<button onClick={ this.openMessagesPopup }><i className="fa fa-info" aria-hidden="true"></i></button></div>
                </div>            
            </div>
        );        
    }
  }