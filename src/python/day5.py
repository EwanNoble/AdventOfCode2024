#!/usr/bin/env python3

def main(path):

    with open(path, 'r') as file:
        data = file.read().splitlines()

    test = data.index("")
    rules = data[:test]
    pages = data[test+1:]

    rules = [tuple(map(int, rule.split('|'))) for rule in rules]
    allPages = [list(map(int, page.split(','))) for page in pages]

    result = 0
    result2 = 0
    for row in allPages:
        validRow = True
        fixing = True
        while fixing:
            fixing = False
            for i, page in enumerate(row):
                relevantRules = filter(lambda x: x[1] == page, rules)
                for j, followingPage in enumerate(row[i+1:]):
                    for rule in relevantRules:
                        if followingPage == rule[0]:
                            validRow = False
                            row[i], row[i+j+1] = row[i+j+1], row[i]
                            fixing = True
                            break
                if fixing:
                    break
        if validRow:
            result += row[len(row)//2]
        else:
            result2 += row[len(row)//2]

    print(result)
    print(result2)

if __name__ == "__main__":
    main('tests/day5.txt')
    main('inputs/day5.txt')