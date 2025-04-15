using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnhollowerBaseLib;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.Udon.VM;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000078 RID: 120
	public static class UdonDisassembler
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public static void Disassemble(UdonBehaviour udonBehaviour, string ubName)
		{
			UdonDisassembler.Disassemble(udonBehaviour, ubName, null);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public static void Disassemble(UdonBehaviour udonBehaviour, string ubName, IEnumerable<string> targetEventNames)
		{
			try
			{
				IUdonProgram program = udonBehaviour._program;
				Il2CppStructArray<byte> byteCode = program.ByteCode;
				IUdonSymbolTable symbolTable = program.SymbolTable;
				IUdonHeap heap = program.Heap;
				Dictionary<string, List<uint>> eventTable = udonBehaviour._eventTable;
				StringBuilder stringBuilder = new StringBuilder();
				Dictionary<uint, string> dictionary = new Dictionary<uint, string>();
				HashSet<uint> hashSet = new HashSet<uint>();
				Dictionary<uint, string> dictionary2 = new Dictionary<uint, string>();
				bool flag = targetEventNames != null && targetEventNames.Any<string>();
				HashSet<string> hashSet2 = (flag ? new HashSet<string>(targetEventNames, StringComparer.OrdinalIgnoreCase) : null);
				if (eventTable != null)
				{
					foreach (KeyValuePair<string, List<uint>> keyValuePair in eventTable)
					{
						if (!flag || hashSet2.Contains(keyValuePair.key))
						{
							foreach (uint num in keyValuePair.Value)
							{
								hashSet.Add(num);
								dictionary2[num] = keyValuePair.key;
							}
						}
					}
				}
				if (flag && hashSet.Count == 0)
				{
					DeepConsole.Warn("No matching events found for the specified filter in " + ubName);
				}
				else
				{
					Dictionary<uint, uint> dictionary3 = new Dictionary<uint, uint>();
					if (flag)
					{
						List<uint> list = hashSet.OrderBy((uint a) => a).ToList<uint>();
						for (int i = 0; i < list.Count; i++)
						{
							uint num2 = list[i];
							uint num3 = ((i < list.Count - 1) ? list[i + 1] : ((uint)byteCode.Length));
							dictionary3[num2] = num3;
						}
					}
					int j = 0;
					bool flag2 = !flag;
					uint num4 = (uint)byteCode.Length;
					while (j < byteCode.Length)
					{
						try
						{
							if (hashSet.Contains((uint)j))
							{
								string text = dictionary2[(uint)j];
								if (flag)
								{
									flag2 = true;
									num4 = dictionary3[(uint)j];
								}
								stringBuilder.AppendLine(".end_of_func");
								stringBuilder.AppendLine(".func_" + text);
							}
							if (!flag2)
							{
								j += 4;
							}
							else if (flag && (long)j >= (long)((ulong)num4))
							{
								flag2 = false;
							}
							else
							{
								if (j + 4 > byteCode.Length)
								{
									DeepConsole.Warn(string.Format("Reached end of bytecode prematurely at 0x{0:X}.", j));
									break;
								}
								uint num5 = UdonDisassembler.SwapEndianness(BitConverter.ToUInt32(byteCode, j));
								switch (num5)
								{
								case 0U:
									DeepConsole.DebugLog("\nResolving NOP");
									stringBuilder.AppendLine(string.Format("0x{0:X}  NOP", j));
									j += 4;
									continue;
								case 1U:
									if (j + 8 <= byteCode.Length)
									{
										DeepConsole.DebugLog("\nResolving PUSH");
										uint num6 = UdonDisassembler.SwapEndianness(BitConverter.ToUInt32(byteCode, j + 4));
										string text2 = symbolTable.GetSymbolFromAddress(num6) ?? string.Format("0x{0:X}", num6);
										object heapVariable = heap.GetHeapVariable(num6);
										string text3 = ((heapVariable != null) ? heapVariable.GetType().FullName : null) ?? "Unknown";
										if (heapVariable != null && text2.Contains("_const_"))
										{
											Type type = heapVariable.GetType();
											if (type == typeof(bool) || type == typeof(int) || type == typeof(float) || type == typeof(double))
											{
												dictionary[num6] = heapVariable.ToString();
											}
											else if (type == typeof(string))
											{
												dictionary[num6] = string.Format("\"{0}\"", heapVariable);
											}
										}
										stringBuilder.AppendLine(string.Format("0x{0:X}  PUSH 0x{1:X} ({2}[{3}])", new object[] { j, num6, text2, text3 }));
										j += 8;
										continue;
									}
									continue;
								case 2U:
									DeepConsole.DebugLog("\nResolving POP");
									stringBuilder.AppendLine(string.Format("0x{0:X}  POP", j));
									j += 4;
									continue;
								case 4U:
									if (j + 8 <= byteCode.Length)
									{
										DeepConsole.DebugLog("\nResolving JUMP_IF_FALSE");
										uint num7 = UdonDisassembler.SwapEndianness(BitConverter.ToUInt32(byteCode, j + 4));
										stringBuilder.AppendLine(string.Format("0x{0:X}  JUMP_IF_FALSE 0x{1:X}", j, num7));
										j += 8;
										continue;
									}
									continue;
								case 5U:
									if (j + 8 <= byteCode.Length)
									{
										DeepConsole.DebugLog("\nResolving JMP");
										uint num8 = UdonDisassembler.SwapEndianness(BitConverter.ToUInt32(byteCode, j + 4));
										stringBuilder.AppendLine(string.Format("0x{0:X}  JMP 0x{1:X}", j, num8));
										j += 8;
										continue;
									}
									continue;
								case 6U:
									if (j + 8 <= byteCode.Length)
									{
										DeepConsole.DebugLog("\nResolving EXTERN");
										uint num9 = UdonDisassembler.SwapEndianness(BitConverter.ToUInt32(byteCode, j + 4));
										object heapVariable2 = heap.GetHeapVariable(num9);
										string text4 = string.Format("unknown_extern@{0:X}", num9);
										if (heapVariable2 != null)
										{
											UdonVM.CachedUdonExternDelegate cachedUdonExternDelegate = heapVariable2 as UdonVM.CachedUdonExternDelegate;
											if (cachedUdonExternDelegate != null)
											{
												text4 = cachedUdonExternDelegate.externSignature ?? text4;
											}
											else
											{
												text4 = heapVariable2.ToString();
											}
										}
										stringBuilder.AppendLine(string.Format("0x{0:X}  EXTERN \"{1}\"", j, text4));
										j += 8;
										continue;
									}
									continue;
								case 7U:
									if (j + 8 <= byteCode.Length)
									{
										DeepConsole.DebugLog("\nResolving ANNOTATION");
										uint num10 = UdonDisassembler.SwapEndianness(BitConverter.ToUInt32(byteCode, j + 4));
										Object heapVariable3 = heap.GetHeapVariable(num10);
										string text5 = ((heapVariable3 != null) ? heapVariable3.ToString() : null) ?? string.Format("unknown_annotation@{0:X}", num10);
										stringBuilder.AppendLine(string.Format("0x{0:X}  ANNOTATION \"{1}\"", j, text5));
										j += 8;
										continue;
									}
									continue;
								case 8U:
									if (j + 8 <= byteCode.Length)
									{
										DeepConsole.DebugLog("\nResolving JMP_INDIRECT");
										uint num11 = UdonDisassembler.SwapEndianness(BitConverter.ToUInt32(byteCode, j + 4));
										string text6 = (symbolTable.HasSymbolForAddress(num11) ? symbolTable.GetSymbolFromAddress(num11) : string.Format("0x{0:X}", num11));
										stringBuilder.AppendLine(string.Format("0x{0:X}  JMP [{1}]", j, text6));
										j += 8;
										continue;
									}
									continue;
								case 9U:
									DeepConsole.DebugLog("\nResolving COPY");
									stringBuilder.AppendLine(string.Format("0x{0:X}  COPY", j));
									j += 4;
									continue;
								}
								DeepConsole.Log("UdonDBLR", string.Format("Unknown opcode: {0} at address 0x{1:X}", num5, j));
								j += 4;
							}
						}
						catch (Exception ex)
						{
							DeepConsole.Log("UdonDBLR", string.Format("Failed to process bytecode at 0x{0:X}:{1}", j, ex.Message));
							break;
						}
					}
					stringBuilder.AppendLine(".end");
					if (!Directory.Exists(UdonDisassembler.SaveDir))
					{
						Directory.CreateDirectory(UdonDisassembler.SaveDir);
					}
					string text7 = DateTime.Now.ToString("yyyyMMdd_HHmmss");
					string text8 = (flag ? "_filtered" : "");
					string text9 = Path.Combine(UdonDisassembler.SaveDir, string.Concat(new string[] { "Disassembled_", ubName, text8, "_", text7, ".txt" }));
					DeepConsole.Log("UdonDBLR", "Saving to file...");
					File.WriteAllText(text9, stringBuilder.ToString());
					if (dictionary.Count > 0)
					{
						StringBuilder stringBuilder2 = new StringBuilder(dictionary.Count * 20);
						foreach (KeyValuePair<uint, string> keyValuePair2 in dictionary)
						{
							stringBuilder2.AppendLine(string.Format("0x{0:X}: {1}", keyValuePair2.Key, keyValuePair2.Value));
						}
						File.WriteAllText(Path.Combine(UdonDisassembler.SaveDir, string.Concat(new string[] { "Constants_", ubName, text8, "_", text7, ".txt" })), stringBuilder2.ToString());
					}
					string text10 = (flag ? string.Format("(filtered to {0} events)", hashSet.Count) : "");
					DeepConsole.Log("UdonDBLR", "Disassembly completed for " + ubName + text10);
				}
			}
			catch (Exception ex2)
			{
				DeepConsole.Log("UdonDBLR", "Error in UdonDisassembler.");
				DeepConsole.E(ex2);
				throw;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00010674 File Offset: 0x0000E874
		private static uint SwapEndianness(uint value)
		{
			return ((value & 255U) << 24) | ((value & 65280U) << 8) | ((value & 16711680U) >> 8) | ((value & 4278190080U) >> 24);
		}

		// Token: 0x0400015C RID: 348
		private static readonly string SaveDir = Path.Combine(Directory.GetCurrentDirectory(), "DeepClient/UdonDisassembler");
	}
}
