import React, { Component } from 'react';

import api from '../services/api';

import { TouchableOpacity } from 'react-native-gesture-handler';

import AsyncStorage from '@react-native-async-storage/async-storage';
import { View, Text, StyleSheet, FlatList, TextInput, Image } from 'react-native';

export default class Consultas extends Component {
    constructor(props) {
        super(props);
        this.state = {
            listaConsultas: []
        };
    }

    buscarConsultas = async () => {
        const chave = await AsyncStorage.getItem('userToken')


        const resposta = await api.get('/consultas', {
            headers: {
                authorization: 'Bearer ' + chave
            }
        });

        const dadosApi = resposta.data;

        this.setState({ listaConsultas: dadosApi })

    }
    componentDidMount() {
        this.buscarConsultas();
    }
    render(){
        return(
            <View style={styles.main}>
                <View style={styles.mainHeader}>
                    <View style={styles.mainHeaderRow}>
                        <Text style={styles.mainHeaderText}>{'Consultas'.toUpperCase()}</Text>
                    </View>

                    <View style={styles.mainHeaderLine}></View>
                    </View>

                    
                    <View style={styles.mainBody}>
                    <FlatList
                        contentContainerStyle={styles.mainBodyContent}
                        data={this.state.listaConsultas}
                        keyExtractor={item => item.idConsulta}
                        renderItem={this.renderItem}
                    />
                </View>
            </View>
        )
        
    }

    renderItem = ({ item }) => (
        <View style={styles.flatItemRow}>
            <View style={styles.flatItemContainer}>
                <Text style={styles.flatItemTitle}>Consulta:</Text>
                <Text style={styles.flatItemInfo}>Paciente: {item.idPacienteNavigation.idUsuarioNavigation.nomeUsuario}</Text>
                <Text style={styles.flatItemInfo}>Medico: {item.idMedicoNavigation.idUsuarioNavigation.nomeUsuario}</Text>
                <Text style={styles.flatItemInfo}>{item.descricaoConsulta}</Text>

                <Text style={styles.flatItemInfo}>
                {Intl.DateTimeFormat("pt-BR", {
                                    year: 'numeric', month: 'short', day: 'numeric',
                                    hour: 'numeric', minute: 'numeric', hour12: false
                                }).format(new Date(item.dataConsulta))} 
                </Text>
            </View>

        </View>
)
}

const styles = StyleSheet.create({
    main: {
      flex: 1,
      backgroundColor: '#F1F1F1',
    },
    mainHeader: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'center',
    },
    mainHeaderRow: {
      flexDirection: 'row',
    },
    mainHeaderText: {
      fontSize: 16,
      letterSpacing: 5,
      color: '#1A2980',
    },
    mainHeaderLine: {
      width: 220,
      paddingTop: 10,
      borderColor: '#1A2980',
      borderBottomWidth: 1,
      borderRadius: 20
    },
  
    mainBody: {
      flex: 5,
    },
    mainBodyContent: {
      paddingTop: 30,
      paddingRight: 50,
      paddingLeft: 50,
    },
    flatItemRow: {
      flexDirection: 'row',
      borderWidth: 1,
      borderColor: '#1A2980',
      marginTop: 30,
      borderRadius: 10
    },
    flatItemContainer: {
      flex: 1,
    },
    flatItemTitle: {
      fontSize: 13,
      color: '#fff',
      backgroundColor: '#1A2980',
      borderRadius: 10,
      paddingLeft: 10
    },
    flatItemInfo: {
      fontSize: 12,
      color: '#999',
      lineHeight: 24,
      paddingLeft: 10
    },
    flatItemImg: {
      justifyContent: 'center',
    },
    flatItemImgIcon: {
      width: 26,
      height: 26,
      tintColor: '#B727FF',
    },
});