/**
* Sample React Native App
* https://github.com/facebook/react-native
*
* @format
* @flow strict-local
*/

import React from 'react';
import { StatusBar } from 'react-native';
 
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
 
const AuthStack = createStackNavigator();
 
import Login from './src/screens/login';
import Consultas from './src/screens/Consultas';
 
 export default function Stack() {
   return (
     <NavigationContainer>
       <StatusBar
         hidden={true}
       />
       <AuthStack.Navigator
         initialRouteName="login"
         screenOptions={{
           headerShown: false
         }}>
 
         <AuthStack.Screen name="login" component={Login}/>
         <AuthStack.Screen name="consultas" component={Consultas}/>
       </AuthStack.Navigator>
     </NavigationContainer>
   )
 }
