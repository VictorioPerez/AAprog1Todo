using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMPersonaMio1
{
    public partial class FrmPersona : Form
    {
        Acceso acceso1 = new Acceso();
        List<Persona> personaList = new List<Persona>();
        public FrmPersona()
        {
            InitializeComponent();
        }

        private void FrmPersona_Load(object sender, EventArgs e)
        {
            Habilitar(false);
            CargarCombo1();
            CargarCombo2();
            CargarLista();
        }

        private void CargarLista()
        {
            personaList.Clear();
            lstPersonas.Items.Clear();
            DataTable dt = acceso1.Consultar("SELECT * FROM personas");
            foreach (DataRow dr in dt.Rows)
            {
                Persona p = new Persona();
                p.pApellido = Convert.ToString(dr["apellido"]);
                p.pNombres = Convert.ToString(dr["nombres"]);
                p.pTipoDocumento = Convert.ToInt32(dr["tipo_documento"]);
                p.pDocumento = Convert.ToInt32(dr["documento"]);
                p.pEstadoCivil = Convert.ToInt32(dr["estado_civil"]);
                p.pSexo = Convert.ToInt32(dr["sexo"]);
                p.pFallecio = Convert.ToBoolean(dr["fallecio"]);
                personaList.Add(p);
                lstPersonas.Items.Add(p);
            }
        }

        private void CargarCombo2()
        {
            DataTable dt = acceso1.Consultar("SELECT * FROM estado_civil");
            cboEstadoCivil.DataSource = dt;
            cboEstadoCivil.ValueMember = "id_estado_civil";
            cboEstadoCivil.DisplayMember = "n_estado_civil";
        }

        private void CargarCombo1()
        {
            DataTable dt = acceso1.Consultar("SELECT * FROM tipo_documento");
            cboTipoDocumento.DataSource = dt;
            cboTipoDocumento.ValueMember = "id_tipo_documento";
            cboTipoDocumento.DisplayMember = "n_tipo_documento";
        }

        private void Habilitar(bool X)
        {
            btnNuevo.Enabled = !X;
            btnEditar.Enabled = !X;
            btnBorrar.Enabled = !X;
            btnGrabar.Enabled = X;
            btnCancelar.Enabled = X;
            txtNombres.Enabled = X;
            txtApellido.Enabled = X;
            txtDocumento.Enabled = X;
            cboEstadoCivil.Enabled = X;
            cboTipoDocumento.Enabled = X;
            chkFallecio.Enabled = X;
            rbtFemenino.Enabled = X;
            rbtMasculino.Enabled = X;
        }

        private void Limpiar()
        {
            txtApellido.Text = "";
            txtNombres.Text = "";
            txtDocumento.Text = "";
            cboTipoDocumento.SelectedIndex = -1;
            cboEstadoCivil.SelectedIndex = -1;
            rbtFemenino.Checked = false;
            rbtMasculino.Checked = false;
            chkFallecio.Checked = false;
        }
        private void lstPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCampos(lstPersonas.SelectedIndex);
        }

        private void CargarCampos(int i)
        {
            txtApellido.Text = personaList[i].pApellido;
            txtNombres.Text = personaList[i].pNombres;
            cboTipoDocumento.SelectedValue = personaList[i].pTipoDocumento;
            txtDocumento.Text = personaList[i].pDocumento.ToString();
            cboEstadoCivil.SelectedValue = personaList[i].pEstadoCivil;
            if(personaList[i].pSexo == 1)
            {
                rbtFemenino.Checked = true;
            }
            else
            {
                rbtMasculino.Checked = true;
            }
            chkFallecio.Checked = personaList[i].pFallecio;

            /* Si no esta chekeado el chked, q no este habilidato el combo box 
             *Caso especifico que el profe pida si no esta chequeado que no se active el combo box
             *sino, no hace falta hacerlo
             
            if (pacientes[posicion].pObraSocial == 0)
            {
                chkObraSocial.Checked = false;
            }
            else
            {
                chkObraSocial.Checked = true;
                cboObraSocial.SelectedValue = pacientes[posicion].pObraSocial;
                cboObraSocial.Enabled = false;
            }
            */
        }

        private bool Validar()
        {
            bool v = true;
            if(txtApellido.Text == "")
            {
                MessageBox.Show("Ingrese un Apellido correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v = false;
                txtApellido.Focus(); 
            }
            if(txtNombres.Text == "")
            {
                MessageBox.Show("Ingrese un Nombre Correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v = false;
                txtNombres.Focus();
            }
            if(cboTipoDocumento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un Tipo de Documento", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v = false;
                cboTipoDocumento.Focus();
            }
            if(txtDocumento.Text == "")
            {
                MessageBox.Show("Ingrese un Documento correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v = false;
                txtDocumento.Focus();
            }
            else
            {
                try
                {
                    Convert.ToInt32(txtDocumento.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ingrese en valores numericos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDocumento.Focus();
                }
            }
            if(cboEstadoCivil.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un Estado Civil correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v = false;
                cboEstadoCivil.Focus();
            }
            if(!rbtFemenino.Checked && !rbtMasculino.Checked)
            {
                MessageBox.Show("Seleccione un Sexo correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v = false;
                rbtFemenino.Focus();
                rbtMasculino.Focus();
            }
            return v;
        }
        private bool Existe(Persona nueva)
        {
            for(int i = 0; i <personaList.Count; i++)
            {
                if (personaList[i].pDocumento == nueva.pDocumento)
                {
                    return true;
                }
            }
            return false;
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if(Validar())
            {
                Persona p = new Persona();
                p.pApellido = txtApellido.Text;
                p.pNombres = txtNombres.Text;
                p.pTipoDocumento = Convert.ToInt32(cboTipoDocumento.SelectedValue);
                p.pDocumento = Convert.ToInt32(txtDocumento.Text);
                p.pEstadoCivil = Convert.ToInt32(cboEstadoCivil.SelectedValue);
                if(rbtFemenino.Checked)
                {
                    p.pSexo = 1;
                }
                else
                {
                    p.pSexo = 2;
                }
                p.pFallecio = chkFallecio.Checked;

                if(!Existe(p))
                {
                    string insertDB = "INSERT INTO personas VALUES('" + p.pApellido + "','"
                                                                      + p.pNombres + "',"
                                                                      + p.pTipoDocumento + ","
                                                                      + p.pDocumento + ","
                                                                      + p.pEstadoCivil + ","
                                                                      + p.pSexo + ",'"
                                                                      + p.pFallecio + "')";
                    acceso1.oDB(insertDB);
                    MessageBox.Show("Se ingreso la persona correctamente","AVISO",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    CargarLista();
                }
                else
                {
                    string updateDB = "UPDATE personas SET apellido = '" + p.pApellido + "'," +
                                                          "nombres = '" + p.pNombres + "'," +
                                                          "tipo_documento = " + p.pTipoDocumento + "," +
                                                          "estado_civil = " + p.pEstadoCivil + "," +
                                                          "sexo = " + p.pSexo + "," +
                                                          "fallecio = '" + p.pFallecio + "' WHERE documento = " + p.pDocumento;
                    acceso1.oDB(updateDB);
                    MessageBox.Show("Se actualizo la persona correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarLista();
                }
                Habilitar(false);
                Limpiar();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Persona p = new Persona();
            p.pApellido = txtApellido.Text;
            p.pNombres = txtNombres.Text;
            p.pTipoDocumento = Convert.ToInt32(cboTipoDocumento.SelectedValue);
            p.pDocumento = Convert.ToInt32(txtDocumento.Text);
            p.pEstadoCivil = Convert.ToInt32(cboEstadoCivil.SelectedValue);
            if (rbtFemenino.Checked)
            {
                p.pSexo = 1;
            }
            else
            {
                p.pSexo = 2;
            }
            p.pFallecio = chkFallecio.Checked;
            if(MessageBox.Show("Seguro que desea eliminar esta persona?","AVISO",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string deleteDB = "DELETE personas WHERE documento = " + p.pDocumento;
                acceso1.oDB(deleteDB);
            }
            MessageBox.Show("Persona eliminada correctamente", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarLista();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            Limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            txtDocumento.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Habilitar(false);
            Limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Seguro que desea salir?","SALIR",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Close();
            }
        }

        /*Metodo para que aparesca en el combo box la palabra "Particular" en caso de ser pedido
        private void chkObraSocial_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkObraSocial.Checked)
            {
                cboObraSocial.DataSource = null;
                cboObraSocial.Items.Add("Particular");
                cboObraSocial.SelectedItem = "Particular";
                cboObraSocial.Enabled = false;
            }
            else
            {
                CargarCombo();
                cboObraSocial.Enabled = true;
            }
        }
        */
    }
}
