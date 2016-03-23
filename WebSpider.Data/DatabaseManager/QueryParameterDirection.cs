using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Data.DatabaseManager
{
    #region Enum: Parameter Directions
    /// <summary>
    /// Enum for Parameter Directions
    /// </summary>
    public enum QueryParameterDirection : int
    {
        /// <summary>
        /// The parameter is an input parameter.
        /// </summary>
        Input = 1,
        /// <summary>
        /// The parameter is capable of both input and output.
        /// </summary>
        Output = 2,
        /// <summary>
        /// The parameter represents a return value from an 
        /// operation such as a stored procedure, built-in
        /// function, or user-defined function.
        /// </summary>
        Return = 3,
        InputOutput = 4
    }
    #endregion
}
