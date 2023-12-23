using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace hw06
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Port=3306;Database=netschool;Uid=root;Pwd=BrokenmySQL;";

        public void InitializeDatabase()
        {
            return;
        }
        
        public DataTable GetAllSchools()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Schools";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        
        public void AddSchool(string schoolName)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = "INSERT INTO Schools (SchoolName) VALUES (@SchoolName)";
            using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@SchoolName", schoolName);
                command.ExecuteNonQuery();
            }
        }
    }

        public void DeleteSchool(int schoolId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                
                // 在删除学校之前，先删除与该学校相关的学生记录
                string deleteStudentsQuery = "DELETE FROM Students WHERE ClassroomId IN (SELECT ClassroomId FROM Classrooms WHERE SchoolId = @SchoolId)";
                using (MySqlCommand commandStudents = new MySqlCommand(deleteStudentsQuery, connection))
                {
                    commandStudents.Parameters.AddWithValue("@SchoolId", schoolId);
                    commandStudents.ExecuteNonQuery();
                }

                // 在删除学校之前，先删除与该学校相关的班级记录
                string deleteClassroomsQuery = "DELETE FROM Classrooms WHERE SchoolId = @SchoolId";
                using (MySqlCommand commandClassrooms = new MySqlCommand(deleteClassroomsQuery, connection))
                {
                    commandClassrooms.Parameters.AddWithValue("@SchoolId", schoolId);
                    commandClassrooms.ExecuteNonQuery();
                }

                

                // 然后再删除学校记录
                string deleteSchoolQuery = "DELETE FROM Schools WHERE SchoolId = @SchoolId";
                using (MySqlCommand commandSchool = new MySqlCommand(deleteSchoolQuery, connection))
                {
                    commandSchool.Parameters.AddWithValue("@SchoolId", schoolId);
                    commandSchool.ExecuteNonQuery();
                }
            }
        }

    public void UpdateSchool(int schoolId, string newSchoolName)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string updateQuery = "UPDATE Schools SET SchoolName = @NewSchoolName WHERE SchoolId = @SchoolId";
            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@NewSchoolName", newSchoolName);
                command.Parameters.AddWithValue("@SchoolId", schoolId);
                command.ExecuteNonQuery();
            }
        }
    }

    public DataTable SearchSchools(string keyword)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Schools WHERE SchoolName LIKE @Keyword";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
    
    public void AddClassroom(int schoolId, string className)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = "INSERT INTO Classrooms (SchoolId, ClassName) VALUES (@SchoolId, @ClassName)";
            using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@SchoolId", schoolId);
                command.Parameters.AddWithValue("@ClassName", className);
                command.ExecuteNonQuery();
            }
        }
    }
    
    // 删除班级
    public void DeleteClassroom(int classroomId)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            // 在删除班级之前，先删除与该班级相关的学生记录
            string deleteStudentsQuery = "DELETE FROM Students WHERE ClassroomId = @ClassroomId";
            using (MySqlCommand commandStudents = new MySqlCommand(deleteStudentsQuery, connection))
            {
                commandStudents.Parameters.AddWithValue("@ClassroomId", classroomId);
                commandStudents.ExecuteNonQuery();
            }

            // 然后再删除班级记录
            string deleteClassroomQuery = "DELETE FROM Classrooms WHERE ClassroomId = @ClassroomId";
            using (MySqlCommand commandClassroom = new MySqlCommand(deleteClassroomQuery, connection))
            {
                commandClassroom.Parameters.AddWithValue("@ClassroomId", classroomId);
                commandClassroom.ExecuteNonQuery();
            }
        }
    }
    
    // 更新班级
    public void UpdateClassroom(int classroomId, string newClassName)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string updateQuery = "UPDATE Classrooms SET ClassName = @NewClassName WHERE ClassroomId = @ClassroomId";
            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@NewClassName", newClassName);
                command.Parameters.AddWithValue("@ClassroomId", classroomId);
                command.ExecuteNonQuery();
            }
        }
    }
    
    // 搜索班级
    public DataTable SearchClassrooms(string keyword)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Classrooms WHERE ClassName LIKE @Keyword";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
    
    // 获取所有班级
    public DataTable GetAllClassrooms()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Classrooms";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
    
    // 添加学生
    public void AddStudent(int classroomId, string studentName)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = "INSERT INTO Students (ClassroomId, StudentName) VALUES (@ClassroomId, @StudentName)";
            using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@ClassroomId", classroomId);
                command.Parameters.AddWithValue("@StudentName", studentName);
                command.ExecuteNonQuery();
            }
        }
    }
    
    // 删除学生
    public void DeleteStudent(int studentId)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string deleteQuery = "DELETE FROM Students WHERE StudentId = @StudentId";
            using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.ExecuteNonQuery();
            }
        }
    }
    
    // 更新学生
    public void UpdateStudent(int studentId, string newStudentName)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string updateQuery = "UPDATE Students SET StudentName = @NewStudentName WHERE StudentId = @StudentId";
            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@NewStudentName", newStudentName);
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.ExecuteNonQuery();
            }
        }
    }
    
    // 搜索学生
    public DataTable SearchStudents(string keyword)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Students WHERE StudentName LIKE @Keyword";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
    
    // 获取所有学生
    public DataTable GetAllStudents()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Students";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
    
    

    
        
    }
}