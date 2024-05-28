using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class GridViewDemo : System.Web.UI.Page
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
                LoadDropList();
            }
        }

        // Carga los datos de los usuarios que estan registrados en la BD
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                _sqlCommand = new SqlCommand("LeerUsuarios", conn);
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                _dtSet = new DataSet();
                _sqlDataAdapter.Fill(_dtSet);

                userData.DataSource = _dtSet;
                userData.DataBind();
            }
        }

        // Carga la lista de las tablas de Departamento y Direccion
        private void LoadDropList()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("LeerDepartamento", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlDepartamento.DataSource = dr;
                    ddlDepartamento.DataTextField = "Nombre";
                    ddlDepartamento.DataValueField = "DepartamentoID";
                    ddlDepartamento.DataBind();
                    ddlDepartamento.Items.Insert(0, new ListItem("-- Seleccione un Departamento --", "0"));
                }

                conn.Close();

                conn.Open();
                using (SqlCommand cmd = new SqlCommand("LeerDireccion", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlDireccion.DataSource = dr;
                    ddlDireccion.DataTextField = "Ciudad";
                    ddlDireccion.DataValueField = "DireccionID";
                    ddlDireccion.DataBind();
                    ddlDireccion.Items.Insert(0, new ListItem("-- Seleccione una Dirección --", "0"));
                }
            }
        }

        private void ClearForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtSalary.Text = "";
            ddlDepartamento.SelectedIndex = 0;
            ddlDireccion.SelectedIndex = 0;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();

            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnAdd.Visible = true;
            btnClear.Visible = true;
        }

        // Funcion para agregar un nuevo usuario
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PrimerNombre", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@SegundoNombre", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Salario", decimal.Parse(txtSalary.Text));
                    cmd.Parameters.AddWithValue("@DepartamentoID", int.Parse(ddlDepartamento.SelectedValue));
                    cmd.Parameters.AddWithValue("@DireccionID", int.Parse(ddlDireccion.SelectedValue));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            ClearForm();
            LoadData();
        }

        //Recibe el ID del usuario que se quiere actualizar para llenar los campos con los datos del usuario
        protected void OnGetU(object sender, EventArgs e)
        {
            int ejemplo = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            int id = Convert.ToInt32(userData.DataKeys[ejemplo].Values[0]);

            Session["UserID"] = id;

            LoadUserData(id);

            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Visible = false;
            btnClear.Visible = false;
        }

        // Se encarga de traer los datos del usuario segun el ID y lo manda al formulario
        private void LoadUserData(int id)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UsuariosEspecificos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtFirstName.Text = dr["FirstName"].ToString();
                        txtLastName.Text = dr["LastName"].ToString();
                        txtEmail.Text = dr["Email"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                        ddlDepartamento.SelectedValue = dr["DepartamentoID"].ToString();
                        ddlDireccion.SelectedValue = dr["DireccionID"].ToString();
                    }
                }
            }
        }

        //Funcion para poder actualizar el usuario por medio del ID guardado
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int userID = (int)Session["UserID"];

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ActualizarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PrimerNombre", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@SegundoNombre", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Salario", decimal.Parse(txtSalary.Text));
                    cmd.Parameters.AddWithValue("@DepartamentoID", int.Parse(ddlDepartamento.SelectedValue));
                    cmd.Parameters.AddWithValue("@DireccionID", int.Parse(ddlDireccion.SelectedValue));
                    cmd.Parameters.AddWithValue("@ID", userID);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadData();
            ClearForm();

            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnAdd.Visible = true;
            btnClear.Visible = true;
        }

        // Recibe el ID del usuario que se quiere eliminar y ejecuta la funcion para eliminarlo
        protected void OnGetD(object sender, EventArgs e)
        {
            int ejemplo = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            int id = Convert.ToInt32(userData.DataKeys[ejemplo].Values[0]);
            DeleteUser(id);
            LoadData();
        }

        // Funcion para eliminar el usuario recibiendo el ID
        private void DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EliminarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}