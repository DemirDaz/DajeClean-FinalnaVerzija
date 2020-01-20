using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCLEAN
{
    class EFDataProvider
    {
        #region Nalog
        static public void DodajNalog(Nalog nalog)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                cnt.Nalogs.Add(nalog);
                cnt.SaveChanges();
            }
        }

        static public void IzbrisiNalog(Nalog nalog)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                cnt.Nalogs.Remove(nalog);
                cnt.SaveChanges();
            }
        }

        static public int IzmeniNalog(Nalog nalog)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                bool provera = false;
                foreach (Nalog n in cnt.Nalogs)
                {
                    if (n.username == nalog.username)
                        if(n.IDNaloga != nalog.IDNaloga)
                            provera = true;
                }
                if (!provera)
                {
                    Nalog tmp = cnt.Nalogs.Where(x => x.IDNaloga == nalog.IDNaloga).FirstOrDefault();
                    tmp.password = nalog.password;
                    tmp.imePrezime = nalog.imePrezime;
                    tmp.username = nalog.username;
                    return cnt.SaveChanges();
                }
                else
                    return 0;
                    
            }
        }
        #endregion

      
      

        static public void DodajArhiviraniPosao(ArhiviraniPosao posao)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                cnt.ArhiviraniPosaos.Add(posao);
                cnt.SaveChanges();
            }
        }

        static public int IzbrisiArhiviraniPosao(ArhiviraniPosao posao)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                cnt.ArhiviraniPosaos.Remove(posao);
                return cnt.SaveChanges();
            }
        }

        static public int IzmeniArhiviraniPosao(ArhiviraniPosao posao)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                ArhiviraniPosao tmp = cnt.ArhiviraniPosaos.Where(x => x.idPosla == posao.idPosla).FirstOrDefault();
                tmp.tip = posao.tip;
                tmp.vreme = posao.vreme;
                tmp.ulicaIme = posao.ulicaIme;

                return cnt.SaveChanges();
            }
        }

        static public void DodajMoguciPosao(MoguciPosao ulica)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                cnt.MoguciPosaos.Add(ulica);
                cnt.SaveChanges();
            }
        }

        static public int IzbrisiMoguciPosao(MoguciPosao ulica)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                cnt.MoguciPosaos.Remove(ulica);
                return cnt.SaveChanges();
            }
        }

        static public void IzmeniMoguciPosao(MoguciPosao ulica)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                MoguciPosao tmp = cnt.MoguciPosaos.Where(x => x.IDUlice == ulica.IDUlice).FirstOrDefault();
                tmp.povrsina = ulica.povrsina;
                tmp.planp = ulica.planp;
                tmp.ulica = ulica.ulica;
                tmp.Stiklirano = ulica.Stiklirano;
                cnt.SaveChanges();
            }
        }
        static public void RefreshujPoslove() 
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                string danas = DateTime.Now.DayOfWeek.ToString();
                string dplan = "";
                string ddplan = "";
                switch (danas)
                {
                    case "Monday":
                        dplan = "A";
                        ddplan = "E";
                        break;
                    case "Tuesday":
                        dplan = "B";
                        break;
                    case "Wednesday":
                        dplan = "A";
                        ddplan = "F";
                        break;
                    case "Thursday":
                        dplan = "C";
                        break;
                    case "Friday":
                        dplan = "D";
                        break;
                }
                foreach (MoguciPosao p in cnt.MoguciPosaos)
                {
                    if (p.planp != dplan && p.planp != ddplan)
                    {
                        p.Stiklirano = false;
                    }
                }
                cnt.SaveChanges();
            }
        }
        static public Nalog GetNalog(string nalog)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                return cnt.Nalogs.Where(x => x.username == nalog).FirstOrDefault();
            }
        }

        static public List<Nalog> GetNalozi()
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                return cnt.Nalogs.ToList();
            }
        }
        static public ArhiviraniPosao GetArhiviraniPosao(int id)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                return cnt.ArhiviraniPosaos.Where(x => x.idPosla == id).FirstOrDefault();
            }
        }

        static public List<ArhiviraniPosao> GetArhiviraniPoslovi()
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                return cnt.ArhiviraniPosaos.ToList();
            }
        }

        static public MoguciPosao GetMoguciPosao(int ID)
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                return cnt.MoguciPosaos.Where(x => x.IDUlice == ID).FirstOrDefault();
            }
        }

        static public List<MoguciPosao> GetMoguciPoslovi()
        {
            using (DataBaseEntities1 cnt = new DataBaseEntities1())
            {
                return cnt.MoguciPosaos.Where(x => x.Stiklirano == false).ToList();
            }
        }
    

    }
}
