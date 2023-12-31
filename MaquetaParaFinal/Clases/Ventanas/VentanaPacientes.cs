﻿using MaquetaParaFinal.Clases;
using MaquetaParaFinal.Clases.Ventanas;
using MaquetaParaFinal.View.Agregar;
using MaquetaParaFinal.View.Modificar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaquetaParaFinal.View
{
    public partial class Pacientes : Page
    {
        Conectar conectar = new Conectar();

        public Pacientes()
        {
            InitializeComponent();
        }

        private void DataGridPacientes_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID")
            {
                e.Column.Visibility = Visibility.Hidden;
            }
        }

        private void DataGridPacientes_Loaded(object sender, RoutedEventArgs e)
        {
            txtBuscar.Focus();
            try
            {             
                DataGridPacientes.ItemsSource = conectar.DescargaTablaPaciente().DefaultView;
            } catch { }
        }
       
        private void DataGridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridPacientes.SelectedItem != null)
            {
                DataRowView row = (DataRowView)DataGridPacientes.SelectedItem;
                txtNombre.Text = row["Nombre"].ToString();
                txtApellido.Text = row["Apellido"].ToString();
                txtDni.Text = row["Dni"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtFecha_De_Nacimiento.Text = row["Fecha De Nacimiento"].ToString();
                txtTelefono.Text = row["Telefono"].ToString();
                txtCalle.Text = row["Calle"].ToString();
                txtNro.Text = row["Numero"].ToString();
                txtLocalidad.Text = row["Localidad"].ToString();
                txtCodPostas.Text = row["Codigo Postal"].ToString();
                txtPiso.Text = row["Piso"].ToString();

                btModificar.IsEnabled = true;
                btEliminar.IsEnabled = true;
            }
            else
            {
                btModificar.IsEnabled = false;
                btEliminar.IsEnabled = false;
            }
        }

        private void ClickBuscar(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                DataGridPacientes.ItemsSource = conectar.BuscarEnTablaPacientes(txtBuscar.Text).DefaultView;
            }else DataGridPacientes.ItemsSource = conectar.DescargaTablaPaciente().DefaultView;
        }

        private void EnterBuscar(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                if (e.Key == Key.Enter)
                {
                    DataGridPacientes.ItemsSource = conectar.BuscarEnTablaPacientes(txtBuscar.Text).DefaultView;
                }
            }else DataGridPacientes.ItemsSource = conectar.DescargaTablaPaciente().DefaultView;
        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            if (DataGridPacientes.SelectedItem != null)
            {
                DataRowView row = (DataRowView)DataGridPacientes.SelectedItem;
                id = int.Parse(row["ID"].ToString());
            }
            AgregarPaciente agregarPaciente = new AgregarPaciente();
            agregarPaciente.ShowDialog();
            DataGridPacientes.ItemsSource = conectar.DescargaTablaPaciente().DefaultView;
            if (DataGridPacientes.SelectedItem != null) DataGridPacientes.SelectedValue = id;
        }
        private void btModificar_Click(object sender, RoutedEventArgs e)
        {
            ModificarPaciente modificarPaciente = new ModificarPaciente();
            DataRowView row = (DataRowView)DataGridPacientes.SelectedItem;
            int id = int.Parse(row["ID"].ToString());
            modificarPaciente.Id = id;
            modificarPaciente.txtNombre.Text = row["Nombre"].ToString();
            modificarPaciente.txtApellido.Text = row["Apellido"].ToString();
            modificarPaciente.txtDni.Text = row["Dni"].ToString();
            modificarPaciente.txtEmail.Text = row["Email"].ToString();
            modificarPaciente.txtFecha_De_Nacimiento.Text = row["Fecha De Nacimiento"].ToString();
            modificarPaciente.txtTelefono.Text = row["Telefono"].ToString();
            modificarPaciente.txtCalle.Text = row["Calle"].ToString();
            modificarPaciente.txtNro.Text = row["Numero"].ToString();
            modificarPaciente.Localidad = row["Localidad"].ToString();
            modificarPaciente.txtCodPostas.SelectedValue = row["Codigo Postal"];
            modificarPaciente.txtPiso.Text = row["Piso"].ToString();
            modificarPaciente.ShowDialog();
            DataGridPacientes.ItemsSource = conectar.DescargaTablaPaciente().DefaultView;
            DataGridPacientes.SelectedValue = id;
        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text != "Nombre")
            {
                System.Media.SystemSounds.Beep.Play();
                MessageBoxResult resultado = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {txtNombre.Text} {txtApellido.Text}?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    DataRowView row = (DataRowView)DataGridPacientes.SelectedItem;
                    conectar.EliminarPacientes(int.Parse(row["ID"].ToString()));
                    DataGridPacientes.ItemsSource = conectar.DescargaTablaPaciente().DefaultView;
                    Limpiar();
                }
            }
        }

        private void Limpiar() 
        {
            txtNombre.Text = "Nombre";
            txtApellido.Text = "Apellido";
            txtDni.Text = "Dni";
            txtEmail.Text = "Email";
            txtFecha_De_Nacimiento.Text = "Fecha De Nacimiento";
            txtTelefono.Text = "Telefono";
            txtCalle.Text = "Calle";
            txtNro.Text = "Numero";
            txtLocalidad.Text = "Localidad";
            txtCodPostas.Text = "Codigo Postal";
            txtPiso.Text = "Piso";
        }

        private void btnImprimirPaciente_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text != "Nombre") 
            { 
                Impresora impresora = new Impresora();
                impresora.ImprimirDocumento();
            }
            
        }
    }
}
