using System;
using System.Collections.Generic;
using System.Text;

namespace SyteLineDevTools.SyteLine.Connections
{
    /// <summary>
    /// We want to handle these types of connections
    ///     SQL Database (General)
    ///         stored procedures
    ///         tables
    ///             constraints
    ///         triggers
    ///         functions
    ///         table data
    ///     SQL Forms DB
    ///         Forms (Reports)
    ///         Global Scripts
    ///         Component Classes
    ///         Validators
    ///         Strings
    ///         Images
    ///         Explorer Menu
    ///     SQL Objects DB
    ///         IDO Collections
    ///         IDO Methods
    ///         IDO Properties    
    ///     SQL App DB
    ///         Event System
    ///         Users   
    ///     Infor IDO Request Service
    ///         Forms
    ///         Global Scripts
    ///         Component Classes
    ///         Validators
    ///         Strings
    ///         Images
    ///         Explorer Menu
    ///         Table Data
    ///     Local File System
    ///         ** 
    /// We want to be able to Have Input and Output Methods but the connection will be used by all of these objects
    /// 
    /// SQL Server
    ///     server name
    ///     database
    ///     Use Windows Authentication - Integrated Security=SSPI
    ///     user name
    ///     password
    /// Infor IDO Request Service
    ///     Request URL
    ///     Configuration
    ///     User Name
    ///     Password
    ///     Windows Authentication
    /// File System
    ///     Base Directory
    /// </summary>
    public interface IConnection
    {
        string Name { get; set; }
        string Type { get; }
    }
    public interface ISQLConnection : IConnection
    {
        string Server { get; set; }
        string Database { get; set; }
        string UserId { get; set; }
        string Password { get; set; }
        bool IntegratedSecurity { get; set; }
    }
    public interface ISLSQLConnection : ISQLConnection
    {
        string Site { get; set; }
    }
    public interface IIDOConnection : IConnection
    {
        string URL { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Configuration { get; set; }
        string Site { get; set; }
        bool UseWindowAuthentication { get; set; }
    }
    public interface IFileSystemConnection : IConnection
    {
        string BaseDir { get; set; }
    }
}
