using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SimonTathamsPortablePuzzleCollection.Games
{
    class GameSaveFileController
    {
        public void SaveGame()
        {

        }

        public Object LoadGame(string FilePath, Type objectType)
        {
            object tmp = Activator.CreateInstance(objectType);
           
            FileStream stream = new FileStream(FilePath, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                tmp = Convert.ChangeType(formatter.Deserialize(stream),objectType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                stream.Close();
            }
            return tmp;
        }
    }
}
