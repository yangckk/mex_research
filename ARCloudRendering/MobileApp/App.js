/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import React, { Component } from 'react';
import {
  View,
  Text,
} from 'react-native';
import { ViroARSceneNavigator } from 'react-viro';

import AR from './js/AR'

export default class ViroSample extends Component {
  render() {
    return (
      <>
        <ViroARSceneNavigator initialScene={{scene: AR}} />
      </>
    );
  }
};

module.exports = ViroSample
