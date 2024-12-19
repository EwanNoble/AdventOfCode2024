#!/usr/bin/env python3

import copy
import concurrent.futures
import time


def main(path):
    with open(path, 'r') as file:
        lines = file.read().splitlines()

    map = []
    for i, line in enumerate(lines):
        found = line.find('^')
        if found != -1:
            guard = found, i
        map.append(list(line))

    part1map = copy.deepcopy(map)
    result = walk((part1map, '^', guard))

    print(result)

    start_time = time.perf_counter()
    obstructions = 0
    part2inputs = []
    for y, row in enumerate(map):
        for x, element in enumerate(row):
            if element == '.':
                part2map = copy.deepcopy(map)
                part2map[y][x] = '#'
                part2inputs.append((part2map, '^', guard))

    # 946.6260 seconds to beat!
    with concurrent.futures.ProcessPoolExecutor() as pool:
        results = pool.map(walk, part2inputs)
        for result in results:
            if result == None:
                    obstructions += 1

    print(obstructions)
    end_time = time.perf_counter()
    elapsed_time = end_time - start_time
    print(f"Elapsed time: {elapsed_time:.4f} seconds")


def walk(input):
    map = input[0]
    element = input[1]
    x = input[2][0]
    y = input[2][1]
    edgeofmap = False
    height = len(map)
    width = len(map[0])
    movements = []
    while(not edgeofmap):
        if(len(movements) > 100000):
            raise Exception("I think there's a problem")
        match element:
            case '<':
                if x == 0:
                    map[y][x] = 'X'
                    edgeofmap = True
                elif map[y][x-1] == '#':
                    map[y][x] = '^'
                elif map[y][x-1] in ['.', 'X']:
                    map[y][x-1] = element
                    map[y][x] = 'X'
                    x -= 1
            case '>':
                if x == width-1:
                    map[y][x] = 'X'
                    edgeofmap = True
                elif map[y][x+1] == '#':
                    map[y][x] = 'v'
                elif map[y][x+1] in ['.', 'X']:
                    map[y][x+1] = element
                    map[y][x] = 'X'
                    x += 1
                    element = 'v'
            case 'v':
                if y == height-1:
                    map[y][x] = 'X'
                    edgeofmap = True
                elif map[y+1][x] == '#':
                    map[y][x] = '<'
                elif map[y+1][x] in ['.', 'X']:
                    map[y+1][x] = element
                    map[y][x] = 'X'
                    y += 1
                    element = '<'
            case '^':
                if y == 0:
                    map[y][x] = 'X'
                    edgeofmap = True
                elif map[y-1][x] == '#':
                    map[y][x] = '>'
                elif map[y-1][x] in ['.', 'X']:
                    map[y-1][x] = element
                    map[y][x] = 'X'
                    y -= 1
                    element = '>'
        element = map[y][x]
        if (x, y, element) in movements:
            return None
        movements.append((x, y, element))
    return sum(line.count("X") for line in map)

if __name__ == "__main__":
    main('tests/day6.txt')
    main('inputs/day6.txt')