using System;

namespace RPG_character {
    class Program {
        static void Main(string[] args) {
            //testarReacao();
            Console.WriteLine();
            testarPersonagem();
            Console.ReadLine();
        }

        static void testarPersonagem() {
            int totalTestes = 24;
            int testesCorretos = 0;

            Console.WriteLine("### Personagem\n");

            Personagem p = new Personagem(500, 70, 100, 10, 200);
            testesCorretos += rodarTeste("Nivel inicia em 1", p.GetNivel() == 1);
            testesCorretos += rodarTeste("Nivel 1 pode melhorar habilidade", p.MelhorarHabilidade(0));
            testesCorretos += rodarTeste("Nivel 1 pode melhorar apenas uma habilidade", !p.MelhorarHabilidade(0));
            p.AdicionarXp(100);
            testesCorretos += rodarTeste("Personagem pode subir de nivel", p.GetNivel() == 2);
            testesCorretos += rodarTeste("Personagem pode melhorar outra habilidade", p.MelhorarHabilidade(1));
            p.AdicionarXp(50);
            p.AdicionarXp(50);
            testesCorretos += rodarTeste("Personagem sobe de nivel mesmo recebendo experiencia aos poucos", p.GetNivel() == 3);
            testesCorretos += rodarTeste("Personagem nao pode melhorar ultimate antes do nivel 6", !p.MelhorarHabilidade(3));
            p.AdicionarXp(300);
            testesCorretos += rodarTeste("Personagem pode chegar no nivel 6", p.GetNivel() == 6);
            testesCorretos += rodarTeste("Personagem pode melhorar ultimate no nivel 6", p.MelhorarHabilidade(3));
            testesCorretos += rodarTeste("Personagem nao pode usar habilidade com ni­vel de melhoria 0", !p.UsarHabilidade(2));
            p.MelhorarHabilidade(2); // Até aqui: Nível do Personagem: 6. Níveis das Habilidades: { 1, 1, 1, 1 }.
            testesCorretos += rodarTeste("Personagem pode usar habilidade com ni­vel de melhoria > 0", p.UsarHabilidade(2)); // Mana: 490/500.

            p.UsarHabilidade(3); // Mana: 290/500.
            p.UsarHabilidade(3); // Mana: 90/500.

            testesCorretos += rodarTeste("Personagem nao pode usar habilidade se nao tiver mana suficiente", !p.UsarHabilidade(3));

            // Poção
            p.ConsumirPocao(); // Mana: 440/500.
            testesCorretos += rodarTeste("Personagem pode recuperar mana com pocao", p.UsarHabilidade(3)); // Mana: 240/500.
            p.ConsumirPocao(); // Mana: 500/500.
            p.ConsumirPocao(); // tomar outras vezes nao deveria deixar passar dos 500
            p.ConsumirPocao();
            p.ConsumirPocao(); // Mana: 500/500.

            p.UsarHabilidade(3); // Mana: 300/500.
            p.UsarHabilidade(3); // Mana: 100/500.
            p.UsarHabilidade(1); // Mana: 0/500.

            testesCorretos += rodarTeste("Pocao nao recupera alem da mana maxima", !p.UsarHabilidade(2));

            // Niveis máximos
            p.AdicionarXp(2500);
            testesCorretos += rodarTeste("Nao e possi­vel passar do ni­vel 25", p.GetNivel() == 25);
            // Níveis das Habilidades: { 1, 1, 1, 1 }.
            p.MelhorarHabilidade(0); // Níveis das Habilidades: { 2, 1, 1, 1 }.
            p.MelhorarHabilidade(0); // Níveis das Habilidades: { 3, 1, 1, 1 }.
            testesCorretos += rodarTeste("Habilidade 0 chega ao ni­vel 4", p.MelhorarHabilidade(0)); // Níveis das Habilidades: { 4, 1, 1, 1 }.
            testesCorretos += rodarTeste("Habilidade 0 nao passa do nivel 4", !p.MelhorarHabilidade(0));
            p.MelhorarHabilidade(1);
            p.MelhorarHabilidade(1);
            testesCorretos += rodarTeste("Habilidade 1 chega ao nivel 4", p.MelhorarHabilidade(1));
            testesCorretos += rodarTeste("Habilidade 1 nao passa do ni­vel 4", !p.MelhorarHabilidade(1));
            p.MelhorarHabilidade(2);
            p.MelhorarHabilidade(2);
            testesCorretos += rodarTeste("Habilidade 2 chega ao nivel 4", p.MelhorarHabilidade(2));
            testesCorretos += rodarTeste("Habilidade 2 nao passa do nivel 4", !p.MelhorarHabilidade(2));
            p.MelhorarHabilidade(3);
            testesCorretos += rodarTeste("Habilidade 3 chega ao nivel 3", p.MelhorarHabilidade(3));
            testesCorretos += rodarTeste("Habilidade 3 nao passa do nivel 3", !p.MelhorarHabilidade(3));

            p.ConsumirPocao();
            p.ConsumirPocao();
            // mesmo os 500 de mana nao sao suficientes para usar a ult no nivel 3 (200*3 = 600)
            testesCorretos += rodarTeste("Consumo de mana Ã© proporcional ao ni­vel da habilidade", !p.UsarHabilidade(3));

            mostrarResultado(testesCorretos, totalTestes);
        }

        static int rodarTeste(String titulo, bool resultado) {
            Console.WriteLine("- " + (resultado ? "OK" : "X ") + "\t" + titulo);
            return resultado ? 1 : 0;
        }

        static void mostrarResultado(int testesCorretos, int totalTestes) {
            Console.WriteLine("\n> Testes corretos: " + testesCorretos + "/" + totalTestes + " (" + (100 * testesCorretos / totalTestes) + "%)");
            if(testesCorretos == totalTestes) {
                Console.WriteLine("> Tudo certo!!!");
            }
            else {
                Console.WriteLine(">  Ainda falta um pouquinho, mas voce consegue!");
            }
        }
    }
}
