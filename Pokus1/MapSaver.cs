using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using CoreLib;

namespace Pokus1
{
	interface IMapSaver
	{
		void Save(Map map);
	}
	class MapSerializer : IMapSaver
	{
		Stream stream;
		public MapSerializer(Stream stream) => this.stream = stream;
		public void Dispose() => stream.Dispose();
		public void Save(Map map) => Save(map, Json.DefaultSerializer);
		public void Save(Map map, JsonSerializer jsonSerializer)
		{
			StreamWriter sw = new StreamWriter(stream);
			jsonSerializer.Serialize(sw, map, typeof(Map));
			sw.Flush();
			//DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(Map), new List<Type>()
			//{
			//	typeof(Life), typeof(Enemy), typeof(NormalEnemy), typeof(PassiveEnemy),
			//		typeof(Player), typeof(Jumper), typeof(KnifeThrower), typeof(Puddler),
			//	typeof(Animation), typeof(SingleColorAnimation), typeof(NoAnimation),
			//	typeof(Movement), typeof(PlayerMovement), typeof(NoMovement), typeof(UsualMovement),

			//});
			//jsonSer.WriteObject(stream, map);
		}
	}
}
