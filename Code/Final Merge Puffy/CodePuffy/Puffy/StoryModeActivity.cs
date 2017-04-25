using System;
using System.Collections.Generic;
using System.Timers;

namespace PuffyProject {
    class StoryModeActivity {

        protected string activityName = "Story Mode Activity";
        protected string nameStory = "Snowman";

        //All the story scripts (just fake text)...
        private string story = "Fuori c’era un freddo pungente, la maggior parte delle persone non ama questo tipo di clima, preferisce temperature più miti: quando fa freddo se ne sta in casa al caldo. Naturalmente per un pupazzo di neve è tutta un’altra cosa: più l’inverno è rigido, più lui è contento. Se poi si potessero combinare un vento pungente proveniente da est ed una bella bufera di neve, allora sarebbe il massimo. Anche il nostro pupazzo di neve la pensava così: una nebbia gelida, del ghiaccio crepitante e una temperatura di 20 gradi sotto zero non erano ancora abbastanza per lui. Sapete cosa proprio aveva in un uggia? Il sole, non lo sopportava. Nutriva nei suoi confronti una vera e propria avversione. Quando brillava in cielo, a lui veniva uno strano prurito in testa. Il nostro omino di neve stava bene solo quando le nuvole si frapponevano fra lui e il sole; ogni volta in cui questi tornava a fare capolino, lui pensava: “Mmmmm rieccolo! Se ne sta lassù tutto il giorno senza far nulla, eccetto spandere un pò di luce a farmi venire il mal di testa”. “Bau!Bau!”. “Eh? Guarda là chi arriva! Il cagnolino! Beeene!” Il pupazzo di neve amava quel cagnolino, almeno ci si poteva fare una bella chiacchierata. Il cane apparteneva al bambino che aveva costruito il pupazzo. L’animale gli si avvicinò, e “Bau, bau! Ciao pupazzo di neve, come stai? Tutto bene?”. “Beh, così così, starei bene se non ci fosse…”";
        private string question = "\n\nOh no, è successo di nuovo! Bambini aiutatemi, non riesco a ricordare…cosa non sopportava il pupazzo di neve? Il freddo o il sole?";
        private string answer = "\n\nE’ vero! Grazie Marco, bravissimo! Dove eravamo rimasti?";
        private string end = "\n\nAh si! Il pupazzo di neve rispose: “Beh, così così, starei bene se non ci fosse quel maledetto sole, non mi adatterò mai alla sua presenza, non lo sopporto proprio!” Sbalordito, il cane guardò dapprima il pupazzo di neve, poi fissò il cielo e poi di nuovo il suo amico. “Il sole? Ma quale sole?! Io non vedo nessun sole! Intendi dire quella cosa che sta in cielo? Ma quella, omino mio, è la luna! La luna! Possibile che tu non abbia ancora capito la differenza tra il sole e la luna?”. “Sole o luna per me è indifferente, non mi importa di come si chiama quella palla nel cielo, penso che sia una seccatura e basta!”. Il cane fece un paio di giri intorno al pupazzo, dimenando la coda come un forsennato, e poi abbaiò: “Il sole e la luna non sono assolutamente la stessa cosa!”.";

        //Initialize a new Projector Istance...
        Projector projector = new Projector();
        EventArgs e = new EventArgs();

        //Initialize Lights...
        //Put Lights action during story.

        public void run() // Runs the Story activity...
        {

            //tellStory(story, "story");
            //tellStory(question, "question");
            //checkAnswer();
            tellStory(answer, "answer");
            //tellStory(end, "end");
        }

        private void tellStory(string text, string storyMoment) // Tells the Ending of Puffy's Story...
        {
            projector.projectPicture(nameStory, storyMoment);
            PuffySpeak.Instance.speak(text, -1);
        }


        private bool checkAnswer() // To be implemented with the Sensors' Interface. Should check if the left or right sensor is touched...
                                   // Right now always return true, thus automatically selecting answer A.
        {
            //while (true)
            //{
            //    if (getRightSensorInputSignal())
            //    {
            //        return true;
            //    }
            //    if (getLeftSensorInputSignal())
            //    {
            //        return false;
            //    }
            //}
            return true;
        }
    }
}