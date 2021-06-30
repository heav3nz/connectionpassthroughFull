using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConnectionByPass
{
    public class Connection
    {
        //Codigo de Conexion configurada en la BD
        private string _ConnectionString = null;
        private int _CommandTimeout = 0;

        //Constructor que inicializa el codigo de conexion 
        public Connection(string ConnectionString, int CommandTimeout = 0)
        {
            _ConnectionString = ConnectionString;
            _CommandTimeout = CommandTimeout;
        }

        #region Utilidades

        //Metodo Privado que generará la nueva conection string
        private SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_ConnectionString);
        }

        private List<T> GetList<T>(string JSONString)
        {
            List<T> Result = new List<T>();

            Result = JsonConvert.DeserializeObject<List<T>>(JSONString);

            return Result;
        }

        #endregion


        #region Consulta con ADO.NET

        //Variable para controlar las transacciones y manipular el cierre de conexion
        private Boolean _TransactionState = false;

        private SqlConnection _Cnn = null;
        private SqlCommand _sqlComand = null;
        private DataSet _DataSet = null;
        private SqlDataAdapter _sqlDataAdapter = null;
        private SqlTransaction _sqlTransaction = null;

        private SqlConnection Cnn
        {
            get
            {

                //si es null se crea la instancia la primera vez
                if (_Cnn == null)
                    _Cnn = ObtenerConexion();

                //se abre la connexion en caso que este cerrada
                if (_Cnn.State != ConnectionState.Open)
                    _Cnn.Open();

                return _Cnn;
            }
        }

        #endregion


        //Metodo generico que retorna una DataSet desde una consuta o un proceso sin parametros
        public DataSet SqlDataSet(string Query)
        {

            try
            {

                _sqlComand = new SqlCommand(Query, Cnn, _sqlTransaction);
                _sqlComand.CommandTimeout = _CommandTimeout;

                _DataSet = new DataSet();
                _sqlDataAdapter = new SqlDataAdapter(_sqlComand);
                _sqlDataAdapter.Fill(_DataSet);

                return _DataSet;

            }
            catch (Exception ex)
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();

                throw ex;
            }
            finally
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();
            }

        }

        //Metodo generico que retorna una DataTable desde una consuta o un proceso sin parametros
        public DataTable SqlDataTable(string Query)
        {

            try
            {

                _sqlComand = new SqlCommand(Query, Cnn, _sqlTransaction);
                _sqlComand.CommandTimeout = _CommandTimeout;

                _DataSet = new DataSet();
                _sqlDataAdapter = new SqlDataAdapter(_sqlComand);
                _sqlDataAdapter.Fill(_DataSet);

                return _DataSet.Tables[0].Copy();

            }
            catch (Exception ex)
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();

                throw ex;
            }
            finally
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();
            }

        }

        //Metodo generico que retorna una DataTable desde una consuta o un proceso sin parametros
        public DataTableCollection SqlDataTableCollection(string Query)
        {

            try
            {

                _sqlComand = new SqlCommand(Query, Cnn, _sqlTransaction);
                _sqlComand.CommandTimeout = _CommandTimeout;

                _DataSet = new DataSet();
                _sqlDataAdapter = new SqlDataAdapter(_sqlComand);
                _sqlDataAdapter.Fill(_DataSet);

                return _DataSet.Tables;

            }
            catch (Exception ex)
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();

                throw ex;
            }
            finally
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();
            }

        }


        //Metodo generico que retorna una string Json desde una consuta o un proceso sin parametros
        public string SqlQuery(string Query)
        {

            try
            {

                _sqlComand = new SqlCommand(Query, Cnn, _sqlTransaction);
                _sqlComand.CommandTimeout = _CommandTimeout;

                _DataSet = new DataSet();
                _sqlDataAdapter = new SqlDataAdapter(_sqlComand);
                _sqlDataAdapter.Fill(_DataSet);

                string result = JsonConvert.SerializeObject(_DataSet.Tables);
                return result;

            }
            catch (Exception ex)
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();

                throw ex;
            }
            finally
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();
            }

        }

        public List<T> SqlQuery<T>(string Query)
        {

            try
            {

                _sqlComand = new SqlCommand(Query, Cnn, _sqlTransaction);
                _sqlComand.CommandTimeout = _CommandTimeout;

                _DataSet = new DataSet();
                _sqlDataAdapter = new SqlDataAdapter(_sqlComand);
                _sqlDataAdapter.Fill(_DataSet);

                string result = JsonConvert.SerializeObject(_DataSet.Tables[0]);

                return GetList<T>(result);


            }
            catch (Exception ex)
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();

                throw ex;
            }
            finally
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();
            }

        }

        //Metodo generico que retorna string json desde un proceso con parametros
        public string SqlQuery(string sp, params object[] args)
        {

            try
            {


                _sqlComand = new SqlCommand();
                _sqlComand.CommandTimeout = _CommandTimeout;
                _sqlComand.CommandText = sp;
                _sqlComand.CommandType = CommandType.StoredProcedure;
                _sqlComand.Connection = Cnn;
                _sqlComand.Transaction = _sqlTransaction;

                SqlCommandBuilder.DeriveParameters(_sqlComand);

                for (int i = 1; i < _sqlComand.Parameters.Count; i++)
                {
                    _sqlComand.Parameters[i].Value = (args[i - 1] != null) ? args[i - 1] : DBNull.Value;
                }

                _DataSet = new DataSet();
                _sqlDataAdapter = new SqlDataAdapter(_sqlComand);
                _sqlDataAdapter.Fill(_DataSet);

                string result = JsonConvert.SerializeObject(_DataSet.Tables);
                return result;

            }
            catch (Exception ex)
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();

                throw ex;
            }
            finally
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();
            }

        }


        //Metodo que inicializa una transaccion
        public void BeginTransaction()
        {
            _sqlTransaction = Cnn.BeginTransaction();
            _TransactionState = true;
        }


        //Metodo que realiza un RollBack a la transaccion
        public void Rollback()
        {
            if (_TransactionState == true)
            {
                _sqlTransaction.Rollback();
                _sqlTransaction = null;
                _TransactionState = false;

                //se cierra la conexion si se encuentra abierta
                CloseConnection();

            }
        }

        //Metodo que confirma la transaccion
        public void Commit()
        {

            if (_TransactionState == true)
            {

                _sqlTransaction.Commit();
                _sqlTransaction = null;
                _TransactionState = false;

                //se cierra la conexion si se encuentra abierta
                CloseConnection();

            }

        }

        //Metodo que permite cerrar la conexion cuando su estado es Open
        private void CloseConnection()
        {
            if (!_TransactionState)
            {
                if (_Cnn != null)
                    if (_Cnn.State == ConnectionState.Open)
                    {
                        _Cnn.Close();
                        _Cnn.Dispose();
                    }
            }
        }

    }
}
