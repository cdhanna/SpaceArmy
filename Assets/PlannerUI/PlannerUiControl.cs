using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlannerUiControl : MonoBehaviour {

    public int ShipCount = 1;
    public int ActionSlots = 3;

    public int PlayWidth = 24;
    public int PlayHeight = 12;

    public ShipRowControl ShipRowPrefab;
    public GameObject Rows;

    public GameObject GridCellPrefab;
    public GameObject Grid;

    public GameObject SelectionObject;

    private List<ShipRowControl> _shipRows = new List<ShipRowControl>();

    private int SelectedRow, SelectedColumn;

	// Use this for initialization
	void Start () {
		

        // delete all children in rows
        // add ship rows
        for (var i = 0; i < Rows.transform.childCount; i++)
        {
            Destroy(Rows.transform.GetChild(i).gameObject);
        }
        for (var i = 0; i < ShipCount; i++)
        {
            var row = Instantiate(ShipRowPrefab, Rows.transform);
            row.transform.localPosition -= new Vector3(0, i*(row.transform.lossyScale.y + .5f));
            row.ActionCount = ActionSlots;
            row.SetActions();
            _shipRows.Add(row);
        }

        // add grid cells
        for (var i = 0; i < Grid.transform.childCount; i++)
        {
            Destroy(Grid.transform.GetChild(i).gameObject);
        }
        for (var row = 0; row < PlayHeight; row++)
        {
            for (var col = 0; col < PlayWidth; col++)
            {
                var cell = Instantiate(GridCellPrefab, Grid.transform);
                cell.transform.localPosition = new Vector3(col, row);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            SelectedColumn = Mathf.Min(ActionSlots -1, SelectedColumn + 1);
        }
        else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            SelectedColumn = Mathf.Max(0, SelectedColumn - 1);
        }

        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            SelectedRow = Mathf.Max(0, SelectedRow - 1);
        }
        else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            SelectedRow = Mathf.Min(ShipCount - 1, SelectedRow + 1);
        }

        var selectedPos = _shipRows[SelectedRow].GetActionSlotPosition(SelectedColumn);
        SelectionObject.transform.position = selectedPos;

    }
}
