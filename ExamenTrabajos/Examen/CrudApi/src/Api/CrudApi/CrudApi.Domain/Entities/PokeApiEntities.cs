namespace CrudApi.Domain.Entities
{
    public class PokeApiResponse
    {
        public List<PokemonDetail> Results { get; set; } = [];
    }

    public class PokemonResumen
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class PokemonDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseExperience { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<TypeInfo> Types { get; set; } = new();
    }

    public class TypeInfo
    {
        public TypeSlot Type { get; set; }
    }

    public class TypeSlot
    {
        public string Name { get; set; }
    }
}
