namespace Zoo
{
    public interface IAnimalSpawn
    {
        ZooAnimal Spawn();
        bool IsValid();
    }
}