﻿using MaquetaParaFinal.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaquetaParaFinal.View.Agregar
{
    /// <summary>
    /// Interaction logic for AgregarServicio.xaml
    /// </summary>
    public partial class AgregarServicio : Window
    {
        Conectar conectar = new Conectar();
        public AgregarServicio()
        {
            InitializeComponent();
        }
    }
}