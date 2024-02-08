using ClosedXML.Excel;
using EmprestimoEquipamentos.Data;
using EmprestimoEquipamentos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmprestimoEquipamentos.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _db;

          public EmprestimoController( ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimos = _db.Emprestimos;

            return View(emprestimos);
        }
        //para entender melhor que isso só retornar algo GET porque retorna a view colocaremos o httpget

        [HttpGet]
        public IActionResult CadastrarEmprestimo()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CadastrarEmprestimo(EmprestimosModel emprestimoM)
        {
            if(ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimoM);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro de registro realizada com sucesso!";

                return RedirectToAction("Index");
            }

            return View();
           
        }


        [HttpGet]
        public IActionResult EditarEmprestimo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //vamos armazenar um objeto/registro que é do tipo de EmprestimosModel, no caso que tem as propriedades iguals as dele na tabela.
            EmprestimosModel emprestimosItems = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            return View(emprestimosItems);
        }



        [HttpPost]
        //estou recebendo quando o usuario clica em editar, os valores do forms com os novos valores do item, é o id do item pra ser editado.
        public IActionResult EditarEmprestimo(int id, EmprestimosModel emprestimos)
        {
            if(ModelState.IsValid)
            {
                ///buscando id na tabela do banco que seja igual ao id do item clicado pelo usuario. Apos achar ele retorna o Objeto.
                var editItem = _db.Emprestimos.Find(id);


                if(editItem != null)
                {
                    //alterando os valores do Objeto que está salvo na tabela do banco, com os novos valores que foram recebidos por post, no forms de editarEmprestimo
                    editItem.Recebedor = emprestimos.Recebedor;
                    editItem.Fornecedor = emprestimos.Fornecedor;
                    editItem.EquipamentoEmprestado = emprestimos.EquipamentoEmprestado;

                    _db.SaveChanges();


                    TempData["MensagemSucesso"] = "Edição de registro realizada com sucesso!";

                    //Ou poderiamos somente fazer isso>
                    //_db.Emprestimos.Update(emprestimos)
                    //_db.SaveChanges();
                    //E mais clean code e funciona da mesma forma, já que estamos recebendo os valores novos editados dos campos do form.

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        [HttpGet]
        public IActionResult ExcluirEmprestimo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemRemove = _db.Emprestimos.Find(id);
           
            return View(itemRemove);
        }

        [HttpPost]
        public IActionResult ExcluirEmprestimo(int id, EmprestimosModel emprestimos)
        {
            if (id != null)
            {
                var emprestimosItems = _db.Emprestimos.FirstOrDefault(x => x.Id == id);


                var removeItem = _db.Remove(emprestimosItems);

                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Exclusão de registro realizada com sucesso!";

                return RedirectToAction("Index");

            }

            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult ExportarEmprestimo()
        {
            var dados = GetDados();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados Empréstimo");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);

                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimos.xls");
                }
            }
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados Emprestimo";

            dataTable.Columns.Add("Recebedor", typeof(string));

            dataTable.Columns.Add("Fornecedor", typeof(string));

            dataTable.Columns.Add("EquipamentoEmprestado", typeof(string));

            dataTable.Columns.Add("Data Empréstimo", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if(dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.EquipamentoEmprestado, emprestimo.dataUltimaAtualizacao);
                });
            }

            return dataTable;
        }
    }
}

