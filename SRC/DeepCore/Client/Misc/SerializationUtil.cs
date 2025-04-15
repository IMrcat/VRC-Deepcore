using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Il2CppSystem;
using Il2CppSystem.IO;
using Il2CppSystem.Runtime.Serialization.Formatters.Binary;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000086 RID: 134
	public static class SerializationUtil
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00012144 File Offset: 0x00010344
		public static byte[] ToByteArray(Object obj)
		{
			byte[] array;
			if (obj == null)
			{
				array = null;
			}
			else
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream memoryStream = new MemoryStream();
				binaryFormatter.Serialize(memoryStream, obj);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001217C File Offset: 0x0001037C
		public static byte[] ToByteArray(object obj)
		{
			byte[] array;
			if (obj == null)
			{
				array = null;
			}
			else
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream memoryStream = new MemoryStream();
				binaryFormatter.Serialize(memoryStream, obj);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000121B0 File Offset: 0x000103B0
		public static T FromByteArray<T>(byte[] data)
		{
			T t;
			if (data == null)
			{
				t = default(T);
			}
			else
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					t = (T)((object)binaryFormatter.Deserialize(memoryStream));
				}
			}
			return t;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00012204 File Offset: 0x00010404
		public static T IL2CPPFromByteArray<T>(byte[] data)
		{
			T t;
			if (data == null)
			{
				t = default(T);
			}
			else
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream memoryStream = new MemoryStream(data);
				t = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			return t;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001223F File Offset: 0x0001043F
		public static T FromIL2CPPToManaged<T>(Object obj)
		{
			return SerializationUtil.FromByteArray<T>(SerializationUtil.ToByteArray(obj));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001224C File Offset: 0x0001044C
		public static T FromManagedToIL2CPP<T>(object obj)
		{
			return SerializationUtil.IL2CPPFromByteArray<T>(SerializationUtil.ToByteArray(obj));
		}
	}
}
