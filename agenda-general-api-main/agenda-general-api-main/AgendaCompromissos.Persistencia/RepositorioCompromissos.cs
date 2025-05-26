using System.Text.Json;
using System.Text.Json.Serialization;
using AgendaCompromissos.Model;
using System.IO;
using System.Collections.Generic;

namespace AgendaCompromissos.Persistencial;

public static class RepositorioCompromissos
{
    private static readonly string PastaPersistencia = @"C:\Users\Muril\Downloads\Trabalho OO\-Persistencia-via-console\agenda-general-api-main\agenda-general-api-main\AgendaCompromissos.Persistencia";
    private static readonly string CaminhoArquivo = Path.Combine(PastaPersistencia, "usuarios.json");
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    public static void Salvar(List<Usuario> usuarios)
    {
        if (!Directory.Exists(PastaPersistencia))
            Directory.CreateDirectory(PastaPersistencia);

        var json = JsonSerializer.Serialize(usuarios, Options);
        File.WriteAllText(CaminhoArquivo, json);
    }

    public static List<Usuario> Carregar()
    {
        if (!File.Exists(CaminhoArquivo))
            return new List<Usuario>();

        var json = File.ReadAllText(CaminhoArquivo);
        return JsonSerializer.Deserialize<List<Usuario>>(json, Options) ?? new List<Usuario>();
    }
}