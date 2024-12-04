using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Node
{
    public Vector2 position;           // ������� ���� �� �����
    public List<Node> connections;     // ��������� ���� (�����)
    public bool isStart = false;       // ��������� ����
    public bool isBoss = false;        // ���� � ������
    public Button levelButton;         // ������ ������, ��������� � �����

    public Node(Vector2 pos, bool start = false, bool boss = false)
    {
        position = pos;
        isStart = start;
        isBoss = boss;
        connections = new List<Node>();
    }
}

public class TreeGenerator : MonoBehaviour
{
    public int maxDepth = 5;                      // ������������ ������� ������
    public int branchingFactor = 2;               // ������������ ����� ������ �� ������ ����
    public float nodeSpacing = 100.0f;            // ���������� ����� ������ � UI �����������
    public GameObject buttonPrefab;               // ������ ������ ��� ������
    public Transform mapParent;                   // ������������ ������ ��� ������

    private Node startNode;
    private Node bossNode;
    private List<Node> nodes = new List<Node>();

    void Start()
    {
        GenerateTree();
    }

    void GenerateTree()
    {
        startNode = new Node(Vector2.zero, start: true); // ��������� �����
        nodes.Add(startNode);

        // ������� ������ � ������� ��������, ������� �� ���������� ����
        CreateBranches(startNode, 0);

        // ��������� ����-����
        bossNode = new Node(new Vector2(0, -maxDepth * nodeSpacing), boss: true);
        nodes.Add(bossNode);

        // ��������� ���� �� ������ ����� � ������
        Node lastBranch = nodes[Random.Range(1, nodes.Count - 1)];
        lastBranch.connections.Add(bossNode);
        bossNode.connections.Add(lastBranch);

        // ������� ������ ��� ���� �����
        CreateLevelButtons();
    }

    void CreateBranches(Node parentNode, int depth)
    {
        if (depth >= maxDepth) return;

        // ���������� ���������� ������ ��� �������� ����
        int branchCount = Random.Range(1, branchingFactor + 1);

        for (int i = 0; i < branchCount; i++)
        {
            // ������ ������� ��� ������ ����
            Vector2 newPosition = parentNode.position + new Vector2((i - branchCount / 2) * nodeSpacing, -nodeSpacing);
            Node newNode = new Node(newPosition);

            // ��������� ����� ����� ������
            parentNode.connections.Add(newNode);
            newNode.connections.Add(parentNode);

            nodes.Add(newNode);

            // ���������� ������� ����� ��� ������ ����
            CreateBranches(newNode, depth + 1);
        }
    }

    void CreateLevelButtons()
    {
        foreach (Node node in nodes)
        {
            // ������� ������ ������ ��� ����
            GameObject buttonObj = Instantiate(buttonPrefab, mapParent);
            buttonObj.transform.localPosition = node.position;
            Button levelButton = buttonObj.GetComponent<Button>();
            node.levelButton = levelButton;

            // ������������� ���� ������ � ����������� �� ���� ����
            Color buttonColor = node.isStart ? Color.green : node.isBoss ? Color.red : Color.white;
            buttonObj.GetComponent<Image>().color = buttonColor;

            // ��������� �������� ��� ������� �� ������
            levelButton.onClick.AddListener(() => OnLevelSelected(node));
        }
    }

    void OnLevelSelected(Node node)
    {
        if (node.isStart)
        {
            Debug.Log("������ �����������!");
        }
        else if (node.isBoss)
        {
            Debug.Log("����� � ������!");
        }
        else
        {
            Debug.Log("������� ������!");
        }
    }
}
