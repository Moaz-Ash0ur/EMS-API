﻿using Microsoft.Extensions.Logging;

namespace EMS.DAL.Helper
{
    public class DataAccessException : Exception
    {


        public DataAccessException(Exception ex, string customeMessage, ILogger logger)
        {
            logger.LogError($"main exception {ex.Message} , developer custome exception " +
                $"{customeMessage}");
        }

    }

}
