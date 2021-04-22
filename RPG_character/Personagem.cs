namespace RPG_character {
    class Personagem {
        // Personagem: level, exp, mana.
        int level = 1, exp, mana;

        // Skills : level, cost.
        int[] levelSkill = new int[4];
        int[] costSkills = new int[4];

        int pointsSkills = 0;

        bool firstLevel = true;

        int manaMax;

        public Personagem(int qtdManaMax, int costSkill1, int costSkill2, int costSkill3, int costSkill4) {
            mana = qtdManaMax;
            costSkills[0] = costSkill1;
            costSkills[1] = costSkill2;
            costSkills[2] = costSkill3;
            costSkills[3] = costSkill4;

            manaMax = qtdManaMax;
        }

        // Adiciona uma quantidade de pontos de experiência ao personagem.
        public void AdicionarXp(int pointsExp) {
            exp += pointsExp;

            while(exp > 100 && level < 25) {
                level++;
                exp -= 100;
            }

            if(level == 25 && exp > 100)
                exp = 0;

            if(exp == 100) {
                level++;
                pointsSkills++;
                exp = 0;
            }
        }

        // Retorna o nível atual do personagem
        public int GetNivel() {
            return level;
        }

        // Melhora uma das quatro habilidades do personagem(indexada por 0).
        // Retorna um booleano indicando se foi possível melhorá-la.
        public bool MelhorarHabilidade(int skill) {
            if(level < 6 && skill == 3)
                return false;

            if((levelSkill[skill] == 4 && skill < 3) || (levelSkill[skill] == 3 && skill == 3))
                return false;

            if(pointsSkills > 0 || firstLevel) {
                levelSkill[skill] += 1;
                firstLevel = false;
                return true;
            }

            return false;
        }

        // Ativa a habilidade do personagem (indexada por 0), consumindo mana no processo.
        // Retorna um booleano indicando se foi possível usar a habilidade.
        public bool UsarHabilidade(int skill) {
            if(levelSkill[skill] > 0) {
                int test = costSkills[skill] * levelSkill[skill];
                if(mana >= test) {
                    mana -= test;
                    return true;
                }
            }

            return false;
        }

        // Recarrega a mana do personagem em 350.
        // A mana total não pode ultrapassar a mana máxima.
        public void ConsumirPocao() {
            mana += 350;

            if(mana > manaMax) {
                int difference = mana - manaMax;
                mana -= difference;
            }
        }
    }
}
