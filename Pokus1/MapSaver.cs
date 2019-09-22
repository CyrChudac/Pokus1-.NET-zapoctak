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
		void Save(Environment map);
	}
	class JsonMapSerializer : IMapSaver
	{
		Stream stream;
		public JsonMapSerializer(Stream stream) => this.stream = stream;
		public void Dispose() => stream.Dispose();
		public void Save(Environment map) => Save(map, Json.DefaultSerializer);
		public void Save(Environment map, JsonSerializer jsonSerializer)
		{
			StreamWriter sw = new StreamWriter(stream);
			jsonSerializer.Serialize(sw, map, typeof(Environment));
			sw.Flush();
		}
	}
}
