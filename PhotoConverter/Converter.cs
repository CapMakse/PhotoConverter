using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PhotoConverter
{
    class Converter
    {
        private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"Plugins");
        private string input;
        private string output;
        IReader reader = null;
        IWriter writer = null;
        public Converter(string source, string goal, string output)
        {
            if (source == null) throw new Exception("Вы не выбрали файл для конвертации");
            if (!File.Exists(source)) throw new Exception("Выбранный файл не был найден");
            string inputFormat = source.Substring(source.LastIndexOf(".") + 1);
            if (goal == null) throw new Exception("Вы не выбрали формат для конвертации");
            if (output == null) output = source.Substring(0, source.LastIndexOf(".") + 1) + goal;
            else if (Directory.Exists(output)) output += source.Substring(source.LastIndexOf("\\"), source.LastIndexOf(".") + 1 - source.LastIndexOf("\\")) + goal;
            else throw new Exception("Директории для конечного файла не существует");
            this.input = source;
            this.output = output;

            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();

            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");

            foreach (var file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);

                var readers = asm.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(IReader).FullName).Any());

                var writers = asm.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(IWriter).FullName).Any());

                foreach (var type in readers)
                {
                    var plugin = asm.CreateInstance(type.FullName) as IReader;
                    if (plugin.GetFormat() == inputFormat) reader = plugin; 
                }
                foreach (var type in writers)
                {
                    var plugin = asm.CreateInstance(type.FullName) as IWriter;
                    if (plugin.GetFormat() == goal) writer = plugin;
                }
            }
            if (reader == null) throw new Exception("Вы выбрали файл что не поддерживаеться данной программой");
            if (writer == null) throw new Exception("Выбранный целевой формат не поддерживается");
        }
        public void Convert()
        {
            writer.Write(reader.Read(input), output);
        }
    }
}
