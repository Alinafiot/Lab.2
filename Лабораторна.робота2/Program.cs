using System;
using System.Text.Json;
using System.IO;

class Vector
{
    private double x1, y1, z1;
    private double x2, y2, z2;

    public Vector(double x1, double y1, double z1, double x2, double y2, double z2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.z1 = z1;
        this.x2 = x2;
        this.y2 = y2;
        this.z2 = z2;
    }

    public double X1 { get { return x1; } }
    public double Y1 { get { return y1; } }
    public double Z1 { get { return z1; } }
    public double X2 { get { return x2; } }
    public double Y2 { get { return y2; } }
    public double Z2 { get { return z2; } }

    public double Length()
    {
        double dx = x2 - x1;
        double dy = y2 - y1;
        double dz = z2 - z1;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
    public double Scalardob(Vector other)
    {
        double dx1 = x2 - x1;
        double dy1 = y2 - y1;
        double dz1 = z2 - z1;
        double dx2 = other.x2 - other.x1;
        double dy2 = other.y2 - other.y1;
        double dz2 = other.z2 - other.z1;
        return dx1 * dx2 + dy1 * dy2 + dz1 * dz2;
    }
    public double Cos(Vector other)
    {
        double dotProduct = Scalardob(other);
        double length1 = Length();
        double length2 = other.Length();
        return dotProduct / (length1 * length2);
    }

    public Vector Dodavanna(Vector other)
    {
        double newX2 = x2 + other.x2 - other.x1;
        double newY2 = y2 + other.y2 - other.y1;
        double newZ2 = z2 + other.z2 - other.z1;
        return new Vector(x1, y1, z1, newX2, newY2, newZ2);
    }
    public Vector Vidnimanna(Vector other)
    {
        double newX2 = x2 - other.x2 + other.x1;
        double newY2 = y2 - other.y2 + other.y1;
        double newZ2 = z2 - other.z2 + other.z1;
        return new Vector(x1, y1, z1, newX2, newY2, newZ2);
    }

    public void SaveToJson(string filename)
    {
        string jsonString = JsonSerializer.Serialize(this);
        File.WriteAllText(filename, jsonString);
    }

    public static Vector LoadFromJson(string filename)
    {
        string jsonString = File.ReadAllText(filename);
        return JsonSerializer.Deserialize<Vector>(jsonString);
    }

    public void PrintJsonContent(string filename)
    {
        string jsonContent = File.ReadAllText(filename);
        Console.WriteLine($"Зміст JSON файлу '{filename}':");
        Console.WriteLine(jsonContent);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Введіть координати вектора 1 (x1, y1, z1, x2, y2, z2):");
        string[] coordinates1 = Console.ReadLine().Split(',');
        double x1 = double.Parse(coordinates1[0]);
        double y1 = double.Parse(coordinates1[1]);
        double z1 = double.Parse(coordinates1[2]);
        double x2 = double.Parse(coordinates1[3]);
        double y2 = double.Parse(coordinates1[4]);
        double z2 = double.Parse(coordinates1[5]);

        Console.WriteLine("Введіть координати вектора 2 (x1, y1, z1, x2, y2, z2):");
        string[] coordinates2 = Console.ReadLine().Split(',');
        double u1 = double.Parse(coordinates2[0]);
        double v1 = double.Parse(coordinates2[1]);
        double w1 = double.Parse(coordinates2[2]);
        double u2 = double.Parse(coordinates2[3]);
        double v2 = double.Parse(coordinates2[4]);
        double w2 = double.Parse(coordinates2[5]);

        Vector vector1 = new Vector(x1, y1, z1, x2, y2, z2);
        Vector vector2 = new Vector(u1, v1, w1, u2, v2, w2);

        double scalarProduct = vector1.Scalardob(vector2);
        double cosine = vector1.Cos(vector2);

        Console.WriteLine($"Довжина вектора 1: {vector1.Length()}");
        Console.WriteLine($"Довжина вектора 2: {vector2.Length()}");
        Console.WriteLine($"Скалярний добуток векторів: {scalarProduct}");
        Console.WriteLine($"Косинус кута між векторами: {cosine}");

        Vector sumVector = vector1.Dodavanna(vector2);
        Vector diffVector = vector1.Vidnimanna(vector2);

        Console.WriteLine($"Сума векторів: ({sumVector.X1}, {sumVector.Y1}, {sumVector.Z1}) - ({sumVector.X2}, {sumVector.Y2}, {sumVector.Z2})");
        Console.WriteLine($"Різниця векторів: ({diffVector.X1}, {diffVector.Y1}, {diffVector.Z1})");

        vector1.SaveToJson("vector1.json");
        vector2.SaveToJson("vector2.json");

        Vector loadedVector1 = Vector.LoadFromJson("vector1.json");
        Vector loadedVector2 = Vector.LoadFromJson("vector2.json");

        Console.WriteLine("Збережено у JSON ");
        loadedVector1.PrintJsonContent("vector1.json");
        loadedVector2.PrintJsonContent("vector2.json");
    }
}
