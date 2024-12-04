using System;

public interface IFunction
{
    void EnterCoefficients();
    void EnterX();
    void ShowCoefficients();
    void FindValue();
}

public abstract class FunctionBase : IFunction
{
    public double A0 { get; set; }
    public double A1 { get; set; }
    public double B0 { get; set; }
    public double B1 { get; set; }
    public double X0 { get; set; }

    protected FunctionBase(bool skipInput = false)
    {
        if (!skipInput)
        {
            EnterCoefficients();
            EnterX();
        }
    }

    public abstract void EnterCoefficients();
    public abstract void EnterX();
    public abstract void ShowCoefficients();
    public abstract void FindValue();
}

public class LinearFunction : FunctionBase
{
    public LinearFunction(bool skipInput = false) : base(skipInput) { }

    public override void EnterCoefficients()
    {
        Console.WriteLine("Введіть коефіцієнти для дробово-лінійної функції:");
        Console.Write("a0 = ");
        A0 = Convert.ToDouble(Console.ReadLine());
        Console.Write("a1 = ");
        A1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("b0 = ");
        B0 = Convert.ToDouble(Console.ReadLine());
        Console.Write("b1 = ");
        B1 = Convert.ToDouble(Console.ReadLine());
    }

    public override void EnterX()
    {
        Console.Write("Введіть значення x0 = ");
        X0 = Convert.ToDouble(Console.ReadLine());
    }

    public override void ShowCoefficients()
    {
        Console.WriteLine($"\nДробово-лінійна функція: ({A1}x + {A0}) / ({B1}x + {B0})");
    }

    public override void FindValue()
    {
        double denominator = B1 * X0 + B0;
        if (denominator == 0)
            Console.WriteLine("Знаменник дорівнює 0, обчислення неможливе.");
        else
            Console.WriteLine($"Значення при x0 = {X0}: {(A1 * X0 + A0) / denominator}");
    }
}

public class FractionFunction : FunctionBase
{
    public double A2 { get; set; }
    public double B2 { get; set; }

    public FractionFunction(LinearFunction linearFunction) : base(true)
    {
        A0 = linearFunction.A0;
        A1 = linearFunction.A1;
        B0 = linearFunction.B0;
        B1 = linearFunction.B1;
        X0 = linearFunction.X0;
        EnterAdditionalCoefficients();
    }

    private void EnterAdditionalCoefficients()
    {
        Console.WriteLine("Введіть додаткові коефіцієнти для квадратичної функції:");
        Console.Write("a2 = ");
        A2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("b2 = ");
        B2 = Convert.ToDouble(Console.ReadLine());
    }

    public override void EnterCoefficients() { }

    public override void EnterX() { }

    public override void ShowCoefficients()
    {
        Console.WriteLine($"\nДробова функція: ({A2}x^2 + {A1}x + {A0}) / ({B2}x^2 + {B1}x + {B0})");
    }

    public override void FindValue()
    {
        double denominator = B2 * Math.Pow(X0, 2) + B1 * X0 + B0;
        if (denominator == 0)
            Console.WriteLine("Знаменник дорівнює 0, обчислення неможливе.");
        else
            Console.WriteLine($"Значення при x0 = {X0}: {(A2 * Math.Pow(X0, 2) + A1 * X0 + A0) / denominator}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        LinearFunction linearFunction = new LinearFunction();
        linearFunction.ShowCoefficients();
        linearFunction.FindValue();

        FractionFunction fractionFunction = new FractionFunction(linearFunction);
        fractionFunction.ShowCoefficients();
        fractionFunction.FindValue();
    }
}
