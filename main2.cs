using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2.Pre_ProjectFinal
{
    public partial class main2 : Form
    {
        public main2()
        {
            InitializeComponent();
        }
        // ตั้งค่าเป็นGlobal
        int group1,group2,group3,group4,group5,cost,totalmoney,money,totaltax,sumnogroup4, sumnogroup4ver2,bonus,othermoney;

        private void label15_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void single_CheckedChanged(object sender, EventArgs e)
        {
            if (single.Checked == false)
            {
                couple.Enabled = true;
                child.Enabled = true;
                parent4.Enabled = true;
                parent3.Enabled = true;
                groupcouple.Enabled = true;
                pregnant.Enabled = true;
            }
            else
            {
                couple.Enabled = false;
                child.Enabled = false;
                parent4.Enabled = false;
                parent3.Enabled = false;
                groupcouple.Enabled = false;
                pregnant.Enabled = false;
                noregis.Checked = false;
                couplenomney.Checked = false;
                havemoney.Checked = false;
                parent3.Checked = false;
                parent4.Checked = false;
                childafter2561.Value = 0;
                childbefore2561.Value = 0;
                numericUpDown6.Value = 0;
                radioButton18.Checked = false;
                radioButton22.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //คำนวณภาษีที่ต้องจ่าย
            month_TextChanged(sender, e);
            taxgroup2(sender, e);
            housebuy_ValueChanged(sender, e);
            taxgroup5(sender, e);
            if (totalmoney > 150000 && totalmoney <= 300000)
            {
                totalmoney = (totalmoney - 150000)* 5 / 100;
            }
            else if (totalmoney > 300000 && totalmoney <= 500000)
            {
                totalmoney = ((totalmoney - 300000) * 10 / 100)+7500;
            }
            else if (totalmoney > 500000 && totalmoney <= 750000)
            {
                totalmoney = ((totalmoney-500000) * 15 / 100) + 27500;
            }
            else if (totalmoney > 750000 && totalmoney <= 1000000)
            {
                totalmoney = ((totalmoney-750000) * 20 / 100) + 65000;
            }
            else if (totalmoney > 1000000 && totalmoney <= 2000000)
            {
                totalmoney = ((totalmoney-1000000) * 25 / 100) +115000;
            }
            else if (totalmoney > 2000000 && totalmoney <= 5000000)
            {
                totalmoney = ((totalmoney + 2000000) * 30 / 100) +365000;
            }
            else if (totalmoney > 5000000)
            {
                totalmoney = ((totalmoney - 5000000) * 35 / 100) + 1265000;
            }
            else
            {
                totalmoney = 0;
            }
            allpay.Text = totalmoney.ToString();
        }

        //ภาษีกลุ่มที่ 1
        private void month_TextChanged(object sender, EventArgs e)
        {
            group1 = 0;
            ///สถานภาพ
            if (single.Checked)
            {
                group1 += 60000;
            }
            else if (married.Checked)
            {
                if (noregis.Checked)
                {
                    group1 += 60000;
                }
                else if (havemoney.Checked)
                {
                    group1 += 60000;
                }
                else if (couplenomney.Checked)
                {
                    group1 += 120000;
                }
                else
                {
                    group1 += 60000;
                }
            }
            else
            {
                group1 += 60000;
            }
            //// พ่อแม่ตัวเอง
            if (parent1.Checked)
            {
                group1 += 30000;
            }
            if (parent2.Checked)
            {
                group1 += 30000;
            }
            else
            {
                group1 += 0;
            }
            //// พ่อแม่คู่สมรส
            if (parent3.Checked)
            {
                group1 += 30000;
            }
            if (parent4.Checked)
            {
                group1 += 30000;
            }
            else
            {
                group1 += 0;
            }
            //// ลูกก่อน 61
            int savechildafter61;
            int numericchildafter61 = Convert.ToInt32(childafter2561.Value);
            if (childafter2561.Value > 0)
            {
                if (childbefore2561.Value == 0)
                {
                    savechildafter61 = ((numericchildafter61 - 1) * 60000 + 30000);
                }
                else
                {
                    savechildafter61 = numericchildafter61 * 60000;
                }
            }
            else
            {
                savechildafter61 = 0;
            }
            group1 += savechildafter61;
            int savechildbefore61 = Convert.ToInt32(childbefore2561.Value) * 30000;
            ///ค่าฝากครรภ์
            int preg = Convert.ToInt32(pregnant.Value);
            if (pregnant.Text == "")
            {
                pregnant.Text = "0";
            }
            //ค่าพิการ
            int injure = Convert.ToInt32(pikarn.Value) * 60000;
            if (pikarn.Text == "")
            {
                pikarn.Text = "0";
            }
            ///รวมกลุ่มที่ 1
            group1 = group1 + injure + preg + savechildbefore61;

            ////คำนวณรายได้สุทธิ
            if (month.Text == "")
            {
                month.Text = "0";
            }
            money = int.Parse(month.Text) * 12; ///เงินเดือน * 12 
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            bonus = int.Parse(textBox1.Text);
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            othermoney = Convert.ToInt32(textBox2.Text);

            cost = money * 50 / 100; ///เงินเดือนหักออก 50% เป็นค่าใช้จ่ายแต่ไม่เกิน 100K
            if (cost > 100000)
            {
                cost = 100000;
            }
            moneyyear.Text = money.ToString();
            money = money + bonus + othermoney; ///เก็บค่าเงินที่รวมกับเงินอื่นๆและโบนัส
            totaltax = group1 + group2 + group3 + group5; //เก็บค่าลดหย่อนทุกกลุ่มยกเว้นกลุ่มที่ 4 
            totalmoney = (money - cost - totaltax) - group4; //รายได้สุทธิที่ได้หลังจากหักค่าลดหย่อนทุกกลุ่มหลังจากบริจาค
            if (totalmoney < 0)
            {
                totalmoney = 0;
            }
            total.Text = totalmoney.ToString();
        }

        //ภาษีกลุ่มที่ 2
        private void taxgroup2(Object sender, EventArgs e)
        {
            //ภาษีกลุ่มที่2
            group2 = 0;
            ////ประกันสังคม
            int social = Convert.ToInt32(numericUpDown4.Value);
            if (social >= 9000)
            {
                social = 9000;
            }
            if (numericUpDown4.Text == "")
            {
                numericUpDown4.Text = "0";
            }
            ////ประกันชีวิต
            int life = Convert.ToInt32(numericUpDown5.Value);
            if (life >= 100000)
            {
                life = 100000;
            }
            if (numericUpDown5.Text == "")
            {
                numericUpDown5.Text = "0";
            }
            ////ประกันสุขภาพ
            int body = Convert.ToInt32(numericUpDown7.Value);
            if (body >= 15000)
            {
                body = 15000;
            }
            if (numericUpDown7.Text == "")
            {
                numericUpDown7.Text = "0";
            }
            int praguntotal = life + body;
            ///รวมประกันสุขภาพ + ชีวิต
            if (praguntotal >= 100000)
            {
                praguntotal = 100000;
            }
            ////เบี้ยสุขภาพบิดามารดา
            int dadandmom = Convert.ToInt32(numericUpDown9.Value);
            if (dadandmom >= 15000)
            {
                dadandmom = 15000;
            }
            if (numericUpDown9.Text == "")
            {
                numericUpDown9.Text = "0";
            }
            ////คู่สมรส
            int couple = Convert.ToInt32(numericUpDown6.Value);
            if (couple >= 10000)
            {
                couple = 10000;
            }
            if (numericUpDown6.Text == "")
            {
                numericUpDown6.Text = "0";
            }
            ////เงินกองทุนสำรองเลี้ยงชีพ
            int spare = Convert.ToInt32(numericUpDown8.Value);
            if (numericUpDown8.Text == "")
            {
                numericUpDown8.Text = "0";
            }
            if(spare >= 10000)
            {
                spare = 10000;
            }
            ////เงินสะสมกองทุน
            int stack = Convert.ToInt32(numericUpDown11.Value);
            if (numericUpDown11.Text == "")
            {
                numericUpDown11.Text = "0";
            }
            
            group2 = social + praguntotal + dadandmom + couple + spare + stack;
        }

        ///ปุ่มพ่อแม่
        private void dadmomalive_CheckedChanged(object sender, EventArgs e)
        {
            groupBox10.Enabled = true;
        }

        private void dadmomdead_CheckedChanged(object sender, EventArgs e)
        {
            groupBox10.Enabled = false;
            numericUpDown9.Text = "0";
            numericUpDown9.Value = 0;
        }

        ///ปุ่มคู่สมรส
        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown6.Enabled = false;
            numericUpDown6.Text = "0";
            numericUpDown6.Value = 0;
        }

        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown6.Enabled = true;
        }


        //ภาษีกลุ่มที่ 3
        private void housebuy_ValueChanged(object sender, EventArgs e)
        {
            group3 = 0;
            ///บ้านก่อน 58
            int h58 = Convert.ToInt32(housebuy.Value);
            if (housebuy.Text == "")
            {
                housebuy.Text = "0";
            }
            ///บ้านหลังแรก 58
            int firsthouse58 = Convert.ToInt32(buyhouse58.Value) * 4 / 100;
            if (buyhouse58.Text == "")
            {
                buyhouse58.Text = "0";
            }
            int show5yearhouse58 = Convert.ToInt32(buyhouse58.Value) * 20 / 100;
            showhouse58value20.Text = show5yearhouse58.ToString();
            showhouse58value4.Text = firsthouse58.ToString();
            ///บ้านหลังแรก 62
            int firsthouse62 = Convert.ToInt32(buyhouse62.Value) * 4 / 100;
            if (buyhouse62.Text == "")
            {
                buyhouse62.Text = "0";
            }
            showhouse62value.Text = firsthouse62.ToString();
            group3 = h58 + firsthouse58 + firsthouse62;
        }

        private void af58_CheckedChanged(object sender, EventArgs e)
        {
            groupBox15.Enabled = false;
            groupBox14.Enabled = false;
            buyhouse58.Text = "0";
            buyhouse58.Value = 0;
            buyhouse62.Text = "0";
            buyhouse62.Value = 0;
        }

        private void y58_CheckedChanged(object sender, EventArgs e)
        {
            groupBox15.Enabled = true;
            groupBox14.Enabled = false;
            buyhouse62.Text = "0";
            buyhouse62.Value = 0;
        }

        private void y62_CheckedChanged(object sender, EventArgs e)
        {
            groupBox15.Enabled = false;
            groupBox14.Enabled = true;
            buyhouse58.Text = "0";
            buyhouse58.Value = 0;
        }

        //ภาษีกลุ่มที่ 4 
        private void taxgroup4(Object sender, EventArgs e)
        {
            group4 = 0;

            ///บริจาคสนับสนุนอื่นๆ
            sumnogroup4 = money - cost - totaltax; ///ตัวแปรที่เก็บรายได้สุทธิที่ไม่รวมค่าบริจาค
            int supother = 0;
            int doother = 0;
            int doparty = 0;
            int tenpersup = (sumnogroup4 * 10) / 100; ///หักจาก 10 % ของรายได้สุทธิหลักจากหักค่าใช้จ่ายและค่าลดหย่อนกลุ่มอื่นๆ
            int sup10pershow = (sumnogroup4 * 10 / 100) / 2; ///ตัวแปรที่แสดงยอด 20%
            if (supportother.Text == "")
            {
                supportother.Text = "0";
            }
            supother = int.Parse(supportother.Text) * 2;
            if (supother > tenpersup)
            {
                supother = tenpersup;
                supportother.Text = sup10pershow.ToString();
            }

            ///บริจาคทั่วไป
            if (donateothr.Text == "")
            {
                donateothr.Text = "0";
            }
            doother = int.Parse(donateothr.Text);
            sumnogroup4ver2 = money - cost - totaltax - supother; //ตัวแปรเก็บค่ารายได้สุทธิหลังจากหักค่าใช้จ่าย + ค่าลดหย่อนกลุ่มอื่นๆ และ ค่าสนับสนุนอื่นๆไปแล้ว 
            int sup10perver2 = (sumnogroup4ver2 * 10) / 100; ///หักจาก 10 % ของรายได้สุทธิหลักจากหักค่าใช้จ่าย + ค่าลดหย่อนกลุ่มอื่นๆ และ ค่าสนับสนุนอื่นๆไปแล้ว 
            if (doother > sup10perver2)
            {
                doother = sup10perver2;
                donateothr.Text = sup10perver2.ToString();
            }
            if (donateparty.Text == "")
            {
                donateparty.Text = "0";
            }

            //บริจาคพรรการเมือง
            doparty = Convert.ToInt32(donateparty.Text);
            if (doparty > 10000)
            {
                doparty = 10000;
                donateparty.Text = doparty.ToString();
            }

            //รวมกลุ่ม 4
            group4 = supother + doother + doparty;
        }

        //ภาษีกลุ่มที่ 5
        private void taxgroup5(Object sender, EventArgs e)
        {
            group5 = 0;
            int g51 = int.Parse(shop.Text);
            if (g51 > 15000)
            {
                g51 = 15000;
            }
            if (shop.Text == "")
            {
                shop.Text = "0";
            }
            int g52 = int.Parse(buyedsport.Text);
            if (g52 > 15000)
            {
                g52 = 15000;
            }
            if (buyedsport.Text == "")
            {
                buyedsport.Text = "0";
            }
            int g53 = int.Parse(buybook.Text);
            if (g53 > 15000)
            {
                g53 = 15000;
            }
            if (buybook.Text == "")
            {
                buybook.Text = "0";
            }
            int g54 = int.Parse(buyotop.Text);
            if (g54 > 15000)
            {
                g54 = 15000;
            }
            if (buyotop.Text == "")
            {
                buyotop.Text = "0";
            }
            int g55 = int.Parse(travel1.Text);
            if (g55 > 15000)
            {
                g55 = 15000;
            }
            if (travel1.Text == "")
            {
                travel1.Text = "0";
            }
            int g56 = int.Parse(travel2.Text);
            if (g56 > 20000)
            {
                g56 = 20000;
            }
            if (travel2.Text == "")
            {
                travel2.Text = "0";
            }
            int bmtravel = g55 + g56; ///ค่าเที่ยวเมืองหลักและรองรวมกัน
            if (bmtravel > 20000)
            {
                bmtravel = 20000;
            }
            int g57 = int.Parse(rehome.Text);
            if (g57 > 100000)
            {
                g57 = 100000;
            }
            if (rehome.Text == "")
            {
                rehome.Text = "0";
            }
            int g58 = int.Parse(recar.Text);
            if (g58 > 30000)
            {
                g58 = 30000;
            }
            if (recar.Text == "")
            {
                recar.Text = "0";
            }
            int chrepair = g57 + g58; ///ค่าซ่อมบ้านและซ่อมรถรวมกัน
            if (chrepair > 100000)
            {
                chrepair = 100000;
            }   
            group5 = (g51 + g52 + g53 + g54) + bmtravel + chrepair;
        }

        //ปุ่มรีเซ็ตค่า
        private void button4_Click(object sender, EventArgs e)
        {
            group1 = 0;
            group2 = 0;
            group3 = 0;
            group4 = 0;
            group5 = 0;
            money = 0;
            single.Checked = false;
            married.Checked = false;
            couple.Enabled = false;
            child.Enabled = false;
            childafter2561.Value = 0;
            childbefore2561.Value = 0;
            parent4.Enabled = false;
            parent3.Enabled = false;
            groupcouple.Enabled = false;
            pregnant.Enabled = false;
            pregnant.Value = 0;
            pikarn.Value = 0;
            noregis.Checked = false;
            couplenomney.Checked = false;
            havemoney.Checked = false;
            parent1.Checked = false;
            parent2.Checked = false;
            parent3.Checked = false;
            parent4.Checked = false;
            dadmomalive.Checked = false;
            dadmomdead.Checked = false;
            month.Text = "0";
            moneyyear.Text = "0";
            total.Text = "0";
            allpay.Text = "0";
            numericUpDown10.Value = 0;
            numericUpDown11.Value = 0;
            showhouse62value.Text = "0";
            showhouse58value20.Text = "0";
            showhouse58value4.Text = "0";
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;
            numericUpDown7.Value = 0;
            numericUpDown8.Value = 0;
            numericUpDown9.Value = 0;
            af58.Checked = false;
            y58.Checked = false;
            y62.Checked = false;
            housebuy.Value = 0;
            buyhouse58.Value = 0;
            buyhouse62.Value = 0;
            supportother.Text = "0";
            donateothr.Text = "0";
            donateparty.Value = 0;
            buybook.Value = 0;
            buyedsport.Value = 0;
            buyotop.Value = 0;
            travel1.Value = 0;
            travel2.Value = 0;
            shop.Value = 0;
            recar.Value = 0;
            rehome.Value = 0;
            radioButton18.Checked = false;
            radioButton22.Checked = false;
            radioButton26.Checked = false;
            radioButton27.Checked = false;
            bonus = 0;
            othermoney = 0;
            textBox1.Text = "0";
            textBox2.Text = "0";
        }

        //แก้บัคให้ใส่ได้แค่เฉพาะตัวเลข
        private void month_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
