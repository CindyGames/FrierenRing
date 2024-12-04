using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Node
{
    public Vector2 position;           // Позиция узла на карте
    public List<Node> connections;     // Связанные узлы (ветви)
    public bool isStart = false;       // Начальный узел
    public bool isBoss = false;        // Узел с боссом
    public Button levelButton;         // Кнопка уровня, связанная с узлом

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
    public int maxDepth = 5;                      // Максимальная глубина дерева
    public int branchingFactor = 2;               // Максимальное число ветвей от одного узла
    public float nodeSpacing = 100.0f;            // Расстояние между узлами в UI координатах
    public GameObject buttonPrefab;               // Префаб кнопки для уровня
    public Transform mapParent;                   // Родительский объект для кнопок

    private Node startNode;
    private Node bossNode;
    private List<Node> nodes = new List<Node>();

    void Start()
    {
        GenerateTree();
    }

    void GenerateTree()
    {
        startNode = new Node(Vector2.zero, start: true); // Начальная точка
        nodes.Add(startNode);

        // Создаем дерево с помощью рекурсии, начиная от начального узла
        CreateBranches(startNode, 0);

        // Добавляем босс-узел
        bossNode = new Node(new Vector2(0, -maxDepth * nodeSpacing), boss: true);
        nodes.Add(bossNode);

        // Связываем один из нижних узлов с боссом
        Node lastBranch = nodes[Random.Range(1, nodes.Count - 1)];
        lastBranch.connections.Add(bossNode);
        bossNode.connections.Add(lastBranch);

        // Создаем кнопки для всех узлов
        CreateLevelButtons();
    }

    void CreateBranches(Node parentNode, int depth)
    {
        if (depth >= maxDepth) return;

        // Определяем количество ветвей для текущего узла
        int branchCount = Random.Range(1, branchingFactor + 1);

        for (int i = 0; i < branchCount; i++)
        {
            // Расчет позиции для нового узла
            Vector2 newPosition = parentNode.position + new Vector2((i - branchCount / 2) * nodeSpacing, -nodeSpacing);
            Node newNode = new Node(newPosition);

            // Добавляем связь между узлами
            parentNode.connections.Add(newNode);
            newNode.connections.Add(parentNode);

            nodes.Add(newNode);

            // Рекурсивно создаем ветви для нового узла
            CreateBranches(newNode, depth + 1);
        }
    }

    void CreateLevelButtons()
    {
        foreach (Node node in nodes)
        {
            // Создаем кнопку уровня для узла
            GameObject buttonObj = Instantiate(buttonPrefab, mapParent);
            buttonObj.transform.localPosition = node.position;
            Button levelButton = buttonObj.GetComponent<Button>();
            node.levelButton = levelButton;

            // Устанавливаем цвет кнопки в зависимости от типа узла
            Color buttonColor = node.isStart ? Color.green : node.isBoss ? Color.red : Color.white;
            buttonObj.GetComponent<Image>().color = buttonColor;

            // Назначаем действие при нажатии на кнопку
            levelButton.onClick.AddListener(() => OnLevelSelected(node));
        }
    }

    void OnLevelSelected(Node node)
    {
        if (node.isStart)
        {
            Debug.Log("Начало приключения!");
        }
        else if (node.isBoss)
        {
            Debug.Log("Битва с боссом!");
        }
        else
        {
            Debug.Log("Уровень выбран!");
        }
    }
}
