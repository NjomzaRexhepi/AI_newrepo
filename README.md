# Teknikat e Informuara të Kërkimit (Informed Search Techniques), CSP Problems, SAT Problems
## 1. Blocked N-Queens Problem
Përshkrimi https://shorturl.at/33eJo
<br>
Kërkesat e detyrës:<br>
● Aplikoni A Star (A*) algoritmin për zgjidhjen e këtij problemi!<br>
● Definoni te paktën tri heuristika për zgjidhjen e këtij problemi<br>
● Propozoni nje “admissible” heuristic duke u bazuar ne rezultatet e ofruara nga #2!<br>
<br>
## 2. SAT Problem
Përshkrimi<br>
Në një aheng janë ftuar 100 mysafirë. Mysafirët duhet të ulen në 10 tavolina, secila me nga 10 karriga. Mirëpo ekzistojnë disa kushte për vendosjen e mysafirëve në tavolina. Supozoni se është dhënë lista e mysafirëve të cilët nuk mund të ulen në të njëjtën tavolinë. Lista përbëhet nga çiftet e personave që nuk mund të ulen së bashku (p.sh. nëse lista përmban (M1, M5) - mysafirët, M1 dhe M5 nuk mund të ulen në të njëjtën tavolinë). Gjithashtu supozoni se është dhënë lista e mysafireve që gjithsesi duhet të ulen në të njëjtën tavolinë. Qëllimi është që mysafirët të vendosen në tavolina ashtu që të plotësohen kushtet e dhëna më lartë.<br>
<br>
Kërkesa e detyrës: Të definohet SAT formula për zgjidhjen e këtij problemi.<br>
<br>
## 3. Constraint Programming Problem
Kërkesa e detyrës: Të zgjidhet problemi Killer Sudoku (https://sudoku.com/killer) duke aplikuar Constraint Programming (CSP)
## 4. Teoria e lojrave - MiniMax Algorithm 
Kërkesa e detyrës: Të aplikohet MiniMax algoritmi me Alpha-Beta pruning për lojën e shahut.

Detaje rreth detyrës:
Konfigurimi startues le te jete nje mid-game ku figurat kane levizur maksimalisht pa u be trade
Funksioni i vleresimit le te mbeshtetet ne: Shumen e mundesive te levizjes per secilen figure te lojtarit aktual dhe shperblimin me pike varesisht prej figures qe i ka marr kundershtarit (peshat e figurave i vendosni ju, psh. piuni->1)
I bejme prune levizjet qe nuk kane kuptim me u zgjeru, psh. piuni merr mbretereshen, etj.
Levizjet qe duhet te shtjellohen ne kuader te lojes jane: Leviz MAX, Leviz MIN, Leviz MAX varesisht prej levizjes se MIN (jo me shume)

