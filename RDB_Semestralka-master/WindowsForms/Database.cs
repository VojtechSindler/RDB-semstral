using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WindowsForms
{
    class Database
    {
        private DatabaseModel.DatabaseContext context;

        public Database()
        {
            context = new DatabaseModel.DatabaseContext();
        }

        public int countRows()
        {
            var querry = context.Multiple_data_select;
            int count = querry.Count();
            return count;
        }

        public List<DatabaseModel.Multiple_data_select> search(string SQLquerry)
        {
            try
            {
                var querry = context.Multiple_data_select.SqlQuery(SQLquerry).Skip(0).Take(1000);
                if (querry.Any())
                {
                    return querry.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public object selectSpecific(int skip, int take)
        {
            try
            {
                var querry = (from q in context.Multiple_data_select
                              orderby q.time
                              select new
                              {
                                  q.time,
                                  q.pointID,
                                  q.description,
                                  q.X,
                                  q.Y,
                                  q.value1,
                                  q.value2,
                                  q.PointAccuracy,
                                  q.variableID,
                                  q.variableName,
                                  q.machineID,
                                  q.descriptionMachine,
                                  q.machineAccuracy,
                                  q.groupID,
                                  q.descriptionGroup
                              }).Skip(skip).Take(take);
                if (querry.Any())
                {
                    return querry.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public object selectAll()
        {
            try
            {
                var querry = (from q in context.Multiple_data_select
                              orderby q.time
                              select new
                              {
                                  q.time,
                                  q.pointID,
                                  q.description,
                                  q.X,
                                  q.Y,
                                  q.value1,
                                  q.value2,
                                  q.PointAccuracy,
                                  q.variableID,
                                  q.variableName,
                                  q.machineID,
                                  q.descriptionMachine,
                                  q.machineAccuracy,
                                  q.groupID,
                                  q.descriptionGroup
                              });
                if (querry.Any())
                {
                    return querry;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public object previous(int skip, int take)
        {
            try
            {
                var querry = (from q in context.Multiple_data_select
                              orderby q.time
                              select new
                              {
                                  q.time,
                                  q.pointID,
                                  q.description,
                                  q.X,
                                  q.Y,
                                  q.value1,
                                  q.value2,
                                  q.PointAccuracy,
                                  q.variableID,
                                  q.variableName,
                                  q.machineID,
                                  q.descriptionMachine,
                                  q.machineAccuracy,
                                  q.groupID,
                                  q.descriptionGroup
                              }).Skip(skip).Take(take);
                if (querry.Any())
                {
                    return querry.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public object searchByDate(DateTime date1, DateTime date2)
        {
            try
            {
                var querry = (from q in context.Multiple_data_select
                              orderby q.time
                              where q.time >= date1 && q.time <= date2
                              select new
                              {
                                  q.time,
                                  q.pointID,
                                  q.description,
                                  q.X,
                                  q.Y,
                                  q.value1,
                                  q.value2,
                                  q.PointAccuracy,
                                  q.variableID,
                                  q.variableName,
                                  q.machineID,
                                  q.descriptionMachine,
                                  q.machineAccuracy,
                                  q.groupID,
                                  q.descriptionGroup
                              }).Skip(0).Take(1000);
                if (querry.Any())
                {
                    return querry.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public object searchMachineID(string machineID)
        {
            try
            {
                var querry = (from q in context.Multiple_data_select
                              orderby q.time
                              where q.machineID.Contains(machineID)
                              select new
                              {
                                  q.time,
                                  q.pointID,
                                  q.description,
                                  q.X,
                                  q.Y,
                                  q.value1,
                                  q.value2,
                                  q.PointAccuracy,
                                  q.variableID,
                                  q.variableName,
                                  q.machineID,
                                  q.descriptionMachine,
                                  q.machineAccuracy,
                                  q.groupID,
                                  q.descriptionGroup
                              }).Skip(0).Take(1000);
                if (querry.Any())
                {
                    return querry.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public object searchDeviation(float deviation)
        {
            try
            {
                var querry = (from q in context.Multiple_data_select
                              orderby q.time
                              where Math.Abs(q.value1 - q.value2) <= deviation
                              select new
                              {
                                  q.time,
                                  q.pointID,
                                  q.description,
                                  q.X,
                                  q.Y,
                                  q.value1,
                                  q.value2,
                                  q.PointAccuracy,
                                  q.variableID,
                                  q.variableName,
                                  q.machineID,
                                  q.descriptionMachine,
                                  q.machineAccuracy,
                                  q.groupID,
                                  q.descriptionGroup
                              }).Skip(0).Take(1000);
                if (querry.Any())
                {
                    return querry.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public object searchGroupName(string groupName)
        {
            try
            {
                var querry = (from q in context.Multiple_data_select
                              orderby q.time
                              where q.descriptionGroup.Contains(groupName)
                              select new
                              {
                                  q.time,
                                  q.pointID,
                                  q.description,
                                  q.X,
                                  q.Y,
                                  q.value1,
                                  q.value2,
                                  q.PointAccuracy,
                                  q.variableID,
                                  q.variableName,
                                  q.machineID,
                                  q.descriptionMachine,
                                  q.machineAccuracy,
                                  q.groupID,
                                  q.descriptionGroup
                              }).Skip(0).Take(1000);
                if (querry.Any())
                {
                    return querry.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
    }
}
