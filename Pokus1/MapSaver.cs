using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

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
		public void Save(Map map)
		{
			System.Runtime.Serialization.Json.DataContractJsonSerializer jsonSer
				= new DataContractJsonSerializer(typeof(Map));
			jsonSer.WriteObject(stream, map);
			throw new NotImplementedException();
		}
	}
}
