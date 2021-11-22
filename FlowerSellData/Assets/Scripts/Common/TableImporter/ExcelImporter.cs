using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEditor;
using UnityEngine;

//���� NPOI
using NPOI;
using NPOI.SS.UserModel;
//ǥ�� xls ���� excel��Ʈ
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
//Ȯ�� xlsx ���� excel ��Ʈ
using NPOI.XSSF;
using NPOI.XSSF.UserModel;

namespace KMH
{
    public abstract class ExcelImporter<T> : ScriptableObject where T : ExcelTable, new()
    {
        [Serializable]
        public class DataExportSettings
        {
            //[HorizontalGroup("group", .5f)]
            //[BoxGroup("group/�� �̸�")]
            //[HideLabel]
            //[ReadOnly]
            public string Name;
            //[HorizontalGroup("group")]
            //[BoxGroup("group/���� �̸�")]
            //[HideLabel]
            public string Key;

            public DataExportSettings(string name, string key)
            {
                this.Name = name;
                this.Key = key;
            }
        }

        //[TitleGroup("����")]
        //[LabelText("���� ���")]
        //[Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "xls, xlsx")]
        //[OnValueChanged("LoadExcel")]
        [SerializeField]
        private string filePath;

        private IWorkbook workbook;
        private List<string> sheetNames;

        //[ShowIf("@sheetName!=\"��Ʈ�� �����ϴ�.\"")]
        //[TitleGroup("��Ʈ")]
        //[ValueDropdown("SheetNames")]
        //[LabelText("��Ʈ ����")]
        //[OnValueChanged("LoadSheet")]
        [SerializeField]
        private string sheetName = "��Ʈ�� �����ϴ�.";

        private int colsRowNum = 0;
        private int lastRowNum = 0;

        //[ShowIf("@sheetName!=\"��Ʈ�� �����ϴ�.\"")]
        //[TitleGroup("��Ʈ")]
        //[LabelText("Ű ������ ����")]
        [SerializeField]
        private List<DataExportSettings> exportSettings;

        //[TitleGroup("����")]
        //[Button("@sheetName==\"��Ʈ�� �����ϴ�.\"?\"���� �ε��ϱ�\":\"���� �ٽ� �ε��ϱ�\"", ButtonHeight = 30)]
        private void LoadExcel()
        {
            var version = Path.GetExtension(filePath).Remove(0, 1);
            Debug.Log(version);

            workbook = GetWorkbook(filePath, version);

            sheetNames = new List<string>();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                var sheet = workbook.GetSheetAt(i);
                sheetNames.Add(sheet.SheetName);
            }
            if (workbook.NumberOfSheets > 0)
            {
                sheetName = workbook.GetSheetAt(0).SheetName;
                LoadSheet();
            }
        }

        private void LoadSheet()
        {
            var sheet = workbook.GetSheet(sheetName);
            colsRowNum = 0;
            lastRowNum = 0;

            IRow colsRow = null;
            Debug.Log(sheet.LastRowNum);

            for (int i = sheet.FirstRowNum; i < sheet.LastRowNum + 1; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null)
                {
                    Debug.Log(i);
                    continue;
                }
                var cell = row.GetCell(0);
                if (cell == null)
                {
                    Debug.Log(i);
                }
                if (cell != null && cell.CellType == CellType.String)
                {
                    var value = cell.StringCellValue;
                    Debug.Log(value);
                    if (value == "HEAD")
                    {
                        colsRowNum = i;
                        colsRow = row;
                    }
                    else if (value == "END")
                    {
                        lastRowNum = i;
                    }
                }
            }
            exportSettings = new List<DataExportSettings>();
            if (colsRow == null)
            {
                // ������ �Ǵ� ���� ����
                return;
            }
            foreach (var cell in colsRow.Cells)
            {
                if (cell.StringCellValue == "HEAD")
                    continue;
                if (cell.StringCellValue == "END")
                    break;
                exportSettings.Add(new DataExportSettings(cell.StringCellValue, cell.StringCellValue));
            }
            Debug.Log(lastRowNum);
        }

        // Workbook �о���̱�
        private IWorkbook GetWorkbook(string filename, string version)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                //ǥ�� xls ����
                if ("xls".Equals(version))
                {
                    return new HSSFWorkbook(stream);
                }
                //Ȯ�� xlsx ����
                else if ("xlsx".Equals(version))
                {
                    return new XSSFWorkbook(stream);
                }
                throw new NotSupportedException();
            }
        }

        //[ShowIf("@sheetName!=\"��Ʈ�� �����ϴ�.\"")]
        //[TitleGroup("�ν��Ͻ� ����")]
        //[Button("��Ʈ �����ϱ�", ButtonHeight = 30)]
        protected void GenerateAsset()
        {
            string path = EditorUtility.SaveFilePanel("�����ϱ�", Application.dataPath, sheetName, "asset");

            if (!path.StartsWith(Application.dataPath)) return;

            path = path.Replace(Application.dataPath, "Assets");

            var asset = new T();
            var data = new List<Dictionary<string, object>>();
            var sheet = workbook.GetSheet(sheetName);

            for (int i = colsRowNum + 1; i < lastRowNum; i++)
            {
                var rowData = new Dictionary<string, object>();
                var row = sheet.GetRow(i);
                if (row == null)
                    continue;
                for (int j = 0; j < exportSettings.Count; j++)
                {
                    var settings = exportSettings[j];
                    object value;
                    var cell = row.GetCell(j + 1);
                    if (cell == null)
                        value = null;
                    else
                    {
                        switch (cell.CellType)
                        {
                            case CellType.String:
                                value = cell.StringCellValue;
                                break;
                            case CellType.Numeric:
                                value = cell.NumericCellValue;
                                break;
                            case CellType.Boolean:
                                value = cell.BooleanCellValue;
                                break;
                            default:
                                value = null;
                                break;
                        }
                    }
                    rowData.Add(settings.Key, value);
                }
                data.Add(rowData);
            }
#if UNITY_EDITOR
            asset.Initialized(data);
#endif

            AssetDatabase.CreateAsset(asset, path);
        }
    }
}


