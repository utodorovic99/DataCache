UI je modul nad kojim ce se pozivati sve operacije koje se iniciraju kroz interfejs.
Ima polja FileControler, CacheControler i ConnectionControler.

Korisnik preko UI Metode InitFileLoad prosledjujuci putanju do fajl-a i tip podatka koji se nalzi  
u fajlu. Unutar ove metode poziva se metoda FileControler-a 
LoadFileStoreDB koja za povratnu vrijednost ima ConsumptionUpdate koji se iz UI koristi dalje za 
azuriranje kesiranih podataka. U zavisnosti koji je tip zadatka u putanju poziva se adekvatna 
metoda u nasem sljucaju je to LoadConsumptionStoreDB u kojoj se na osnovu ekstenzije gleda iz 
kojeg tipa fajla se ucitava podatak o potrosnji ( u nasem slucaju je to samo XML ali zbog buducih 
implementacija je raslojeno) instancira odgovarajuci Handler za taj tip fajla i poziva odgovarajucu
metodu za citanje u upis sto je u nasem slucaju XMLConsumptionRead kojoj prosledjujemo putanju. 
Kada pribavimo podatke o potrosnji tada pozivamo dgovarajucu metodu. Za trenutnu
implementaciju jeste samo InitDBConsumptionWrite metoda koju i pozivamo i prosledjujemo
joj ucitanu listu ConsumptionRecord-a (apstrakcija jednog zapisa u dokumentu). Da smo imali
citanje iz nekog drugog tipa dokumenta samo bi pozivali drugaciji handler za 
taj odgovarajuci tip dokumenta, tu bi sve apstrahovali u listu u opet pozvali
InitDBConsumptionWrite.
Da smo imali citanje dokumenta druge strukture onda bi FileControler pozivao neku drugu 
metodu umjesto InitDBConsumptionWrite. Koja struktura dokumenta je u pitanju (koja 
apstrakcija treba da se koristi se prepoznaje iz drugog parametra funkcije iniciranja citanja
koju pozivamo-enum).

InitDBConsumptionWrite poziva metodu konekcionog modula koja je mrezom implementirana na 
modulu baze podataka.

Baza podataka vraca ConsumptionDBWReport koji u sebi nosi niz duplikata, niz izostalih
vrijednosti (morali smo poslati na upis da bi se zapisalo u audit tabelu) i niz novih
geografskih lokacija koje su upisane u bazu a nisu ranije bile upisane kroz UI. Saljemo
ih kao string jer jer navedeno da takve vrste upisa imaju isti ID i ime pa da ne opterecujemo
mrezu saljuci jedan te isti podatak u dva polja. Nizovi indeksa u sebi sadrze indekse 
originalne liste u koju je ucitan sadrzaj iz dokumenta.

Kada se ova vrijednost vrati u InitDBConsumptionWrite tu se ona prepakuje u 
ConsumptionUpdate koji se vraca u UI kao povratna vrijednost funkcije da bi se u Cache
zaveli novi geografski pojmovi i novi Audit podaci.ConsumptionUpdate u sebi sadrzi za svaku
zemlju par nizova sati duplikata i sati koji izostaju.

ConsumptionUpdate kada stigne kroz povratne vrijednosti poziva se metoda 
ConsumptionUpdateHandler koja dovede tabelu geografskih pojmova i audit tabelu u konzistentno
stanje. Te tabele su pod nadleznoscu cache controler-a jer su stalno prisutne iako je teziste
modula na rukovanju kesiranja pretrage.

Baza azuriranja jesu originalni podaci koji se dobavljaju pozivanjem metode InitCacheLoad
CacheControlera unutar konstruktora UI objekta. Ova metoda ce pozvati metode koje dobavljaju
kesirane tabele (u nasem slucaju InitGRecordLoad i InitARecordLoad). Svaki od zahtjeva se 
dalje prosljedjuje na adekvatne metode konekcionog modula.

Sa korisnicke strane geografski zapisi se dodaju kroz UI posredstvom metode
PostGeographicEntity koja provjerava da li je ta zemlja vec evidentirana putem
ContainsGeo metode pa ako to nije slucaj tada u sebi prvo poziva GEntityWrite metodu konekcionog
modula  u kojoj se prvo ta zemlja pokusa upisati u bazu pa ako je to bilo uspjesno evidencija
geografskih podrucja unutar cache-a se azurira posredstvom metode AddNewGeoEntity.

ID Geografskog podrucja se azurira preko UpdateGeo metode UI koja poziva GEntityUpdate 
metodu konekcionog modula gdje se nakon uspjesnog azuriranja u bazi ta vrijednost propagira
i u kesiranu evidenciju preko UpdateGeoEntity metode Cache kontrolera-a.

Metoda GetGeographicEntities vraca listu naziva svih drzava (ne vracam sve jer mi ne treba
kada budem slao upit onda cu u cache-u isparsirati sifru) koju cu namapirati na 
ObservableCollection za UI.

Sam upit baze podataka se realizuje preko InitConsumptionReq metode kojoj se parametri
pretrage salju preko genericke klase ReadReq koja apstrahuje sve naredbe citanja i u sebi
ima samo jedno polje koje je jednako prosledjenom genericckom argumentu sto je u nasem 
slucaju DSpanGeoReq koji u lokalu provjerava da li se nalazi kesiran sadrzaj ili ne. Ako
se tu nalazi direktno se dostavlja i povecava se hitRate za taj zapis (strategija kesiranja)
je izbacivanje po hitRate-u. Ako ga nema onda se poziva metoda ConnectionControler - a
ConsumptionReqPropagate koja iz baze podataka dobavlja trazeni zapis on se vraca nazad a 
pravi se poseban thread sa metodom StoreConsumption ( obezbjediti zastitu od preplitanja 
nad cache-om ) koja skladisti posljednju pretragu ako ima dovoljno mjesta ako nema onda se 
pokrece FreeSpace koji skida onoliko zastarijih zapisa koliko treba
da se obezbjedi novi zapis (kazem vise zapisa jer mogu da skladistim potpun a da 
oslobadjam dva nepotpuna). Takodje prisutan je i thread CacheGarbageCollector() koji se budi
svakih 3 sata, zauzima lock od cache-a i brise sve zastarjele zapise.

